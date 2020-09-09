using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileManager : MonoBehaviour
{
    Factory missileFactory_;
    BuildingManager buildingManager_;

    bool isInitialized_ = false;
    bool isBossPartternAttack = false;

    int maxMissileCount_ = 2;
    int currentMissileCount_;

    float missileSpawnInterval_ = 2f;

    Coroutine spawningMissile_;
    

    List<RecycleObject> missiles_ = new List<RecycleObject>();

    public Action<RecycleObject> MissileDestroyed;
    public Action AllMissilesDestroyed;

    public Factory GetMissileFactory() { return missileFactory_; }

    public void Initialize(Factory missileFactory, BuildingManager buildingManager, int maxMissileCount, float missileSpawnInterval)
    {
        if (isInitialized_)
            return;

        this.missileFactory_ = missileFactory;
        this.buildingManager_ = buildingManager;
        this.maxMissileCount_ = maxMissileCount;
        this.missileSpawnInterval_ = missileSpawnInterval;

        isInitialized_ = true;
    }

    void BindEvents(RecycleObject missileRecycle)
    {
        missileRecycle.Destroyed += OnMissileDestroyed;
        missileRecycle.OutOfScreen += OnMissileOutOfScreen;
        Missile missile = (Missile)missileRecycle;
        missile.BuildingDestroyed += OnMissileBuildingDestroyed;
    }

    void UnBindEvents(RecycleObject missileRecycle)
    {
        missileRecycle.Destroyed -= OnMissileDestroyed;
        missileRecycle.OutOfScreen -= OnMissileOutOfScreen;
        Missile missile = (Missile)missileRecycle;
        missile.BuildingDestroyed -= OnMissileBuildingDestroyed;
    }

    void SpawnMissile()
    {
        Debug.Assert(this.missileFactory_ != null, "missile factory is null!");
        Debug.Assert(this.buildingManager_ != null, "building Manager is null!");

        RecycleObject missileRecycle = missileFactory_.Get();
        missileRecycle.Activate(GetMissileSpawnPosition(), buildingManager_.GetRandomBuildingPosition());

        BindEvents(missileRecycle);

        missiles_.Add(missileRecycle);

        currentMissileCount_++;
    }

    void OnMissileBuildingDestroyed(RecycleObject missileRecycle)
    {
        RestoreMissile(missileRecycle);
        Missile missile = (Missile) missileRecycle;
        missile.BuildingDestroyed?.Invoke(missile);
    }

    void OnMissileDestroyed(RecycleObject missile)
    {
        RestoreMissile(missile);
        MissileDestroyed?.Invoke(missile);
    }

    void OnMissileOutOfScreen(RecycleObject missile)
    {
        RestoreMissile(missile);
    }

    void RestoreMissile(RecycleObject missileRecycle)
    {
        UnBindEvents(missileRecycle);

        Missile missile = (Missile)missileRecycle;
        int index = missiles_.IndexOf(missile);
        missiles_.RemoveAt(index);
        missileFactory_.Restore(missile);

        CheckAllMissileRestored();
    }

    void CheckAllMissileRestored()
    {
        if (currentMissileCount_ == maxMissileCount_ && missiles_.Count == 0)
        {
            AllMissilesDestroyed?.Invoke();
        }
    }

    Vector3 GetMissileSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        spawnPosition.x = UnityEngine.Random.Range(0f, 1f);
        spawnPosition.y = 1f;

        spawnPosition = Camera.main.ViewportToWorldPoint(spawnPosition);
        spawnPosition.z = 0f;
        return spawnPosition;
    }

    Vector3 GetMissileSpawnPosition(float posX)
    {
        Vector3 spawnPosition = Vector3.zero; 
        spawnPosition.y = 1f;

        spawnPosition = Camera.main.ViewportToWorldPoint(spawnPosition);
        spawnPosition.x = posX;
        spawnPosition.z = 0f;
        return spawnPosition;
    }

    public void OnGameStarted()
    {
        currentMissileCount_ = 0;
        spawningMissile_ = StartCoroutine(AutoSpawnMissile());
    }

    IEnumerator AutoSpawnMissile()
    {
        while(currentMissileCount_ < maxMissileCount_)
        {
            yield return new WaitForSeconds(missileSpawnInterval_);

            if (!buildingManager_.HasBuilding)
                yield break;

            SpawnMissile();
        }
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        if (missiles_.Count == 0)
            return;

        foreach (var missile in missiles_)
        {
            missileFactory_.Restore(missile);
        }
    }

    public void OnStageUp(int stageLevel)
    {
        currentMissileCount_ = 0;
        maxMissileCount_ = stageLevel * 2;
        missileSpawnInterval_ = 2f / stageLevel;
        if (stageLevel < 10)
        {
            spawningMissile_ = StartCoroutine(AutoSpawnMissile());
        }
    }

    public void OnBossPatternDoubleAttack()
    {
        Transform[] transforms = buildingManager_.GetBuildingLocators();
        int random = UnityEngine.Random.Range(0, 2);

        if (random == 0)
        {
            RecycleObject leftMissileRecycle = missileFactory_.Get();
            Vector3 spawnMissile = GetMissileSpawnPosition(transforms[0].position.x);
            leftMissileRecycle.Activate(spawnMissile, transforms[0].position);
            BossPatternDoubleAttackSetting(leftMissileRecycle);

            RecycleObject rightMissileRecycle = missileFactory_.Get();
            spawnMissile = GetMissileSpawnPosition(transforms[3].position.x);
            rightMissileRecycle.Activate(spawnMissile, transforms[3].position);
            BossPatternDoubleAttackSetting(rightMissileRecycle);
        }
        else
        {
            RecycleObject leftMissileRecycle = missileFactory_.Get();
            Vector3 spawnMissile = GetMissileSpawnPosition(transforms[1].position.x);
            leftMissileRecycle.Activate(spawnMissile, transforms[1].position);
            BossPatternDoubleAttackSetting(leftMissileRecycle);

            RecycleObject rightMissileRecycle = missileFactory_.Get();
            spawnMissile = GetMissileSpawnPosition(transforms[2].position.x);
            rightMissileRecycle.Activate(spawnMissile, transforms[2].position);
            BossPatternDoubleAttackSetting(rightMissileRecycle);
        }
    }

    public void BossPatternDoubleAttackSetting(RecycleObject recycleObject)
    {
        recycleObject.Destroyed += OnMissileDestroyed;
        recycleObject.OutOfScreen += OnMissileOutOfScreen;
        Missile rightMissile = (Missile)recycleObject;
        rightMissile.BuildingDestroyed += OnMissileBuildingDestroyed;
        missiles_.Add(recycleObject);
    }

    public void OnBossCircleAttack(Transform transform)
    {
        int count = 0;
        int maxCount = UnityEngine.Random.Range(20, 22);
        while (count <= maxCount)
        {
            RecycleObject missileRecycle = missileFactory_.Get();
            missileRecycle.transform.rotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * count / maxCount;
            missileRecycle.transform.Rotate(rotVec);
            missileRecycle.Activate(transform.position);

            BindEvents(missileRecycle);

            missiles_.Add(missileRecycle);
            count += 1;
        }
    }

    public void OnBossPatternFanShapeAttack(float count, int maxCount)
    {
        RecycleObject missileRecycle = missileFactory_.Get();
        missileRecycle.transform.rotation = Quaternion.identity;

        float z = Mathf.Abs(Mathf.Cos(count / maxCount * 10f)) * 120 + 120;

        Vector3 rotVec = Vector3.forward * z;
        missileRecycle.transform.Rotate(rotVec);
        missileRecycle.Activate(transform.position);

        Debug.Log(missileRecycle.transform.position);

        BindEvents(missileRecycle);
        missiles_.Add(missileRecycle);
    }
}
