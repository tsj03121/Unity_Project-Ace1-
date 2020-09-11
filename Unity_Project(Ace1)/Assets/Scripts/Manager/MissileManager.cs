using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileManager : MonoBehaviour
{
    Factory<RecycleObject> missileFactory_;
    BuildingManager buildingManager_;

    bool isInitialized_ = false;
    bool isBossPartternAttack = false;

    int stageLevel_;
    int endStageLevel_;
    int currentMissileCount_;
    int maxMissileCount_ = 2;
    int sortingOrder = 0;
    float missileSpawnInterval_ = 2f;

    Coroutine spawningMissile_;
    

    List<RecycleObject> missiles_ = new List<RecycleObject>();

    public Action<RecycleObject> MissileDestroyed;
    public Action AllMissilesDestroyed;

    public Factory<RecycleObject> GetMissileFactory() { return missileFactory_; }

    public void Initialize(Factory<RecycleObject> missileFactory, BuildingManager buildingManager, int maxMissileCount, float missileSpawnInterval, int endStageLevel)
    {
        if (isInitialized_)
            return;

        missileFactory_ = missileFactory;
        buildingManager_ = buildingManager;
        maxMissileCount_ = maxMissileCount;
        missileSpawnInterval_ = missileSpawnInterval;
        endStageLevel_ = endStageLevel;

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

    void SortingOrderSetting(RecycleObject missileRecycle)
    {
        missileRecycle.GetSpriteRenderer();
        SpriteRenderer spriteRenderer = missileRecycle.GetSpriteRenderer();
        sortingOrder += 1;
        spriteRenderer.sortingOrder = sortingOrder;
    }

    void SpawnMissile()
    {
        RecycleObject missileRecycle = missileFactory_.Get();
        SortingOrderSetting(missileRecycle);
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
        missileFactory_.Restore(missile);
        missiles_.RemoveAt(index);

        CheckAllMissileRestored();
    }

    void CheckAllMissileRestored()
    {
        if (currentMissileCount_ != maxMissileCount_)
            return;

        if (missiles_.Count != 0)
            return;

        if (stageLevel_ % 10 == 0)
            return;

        AllMissilesDestroyed?.Invoke();
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
        OnStageUp(1);
    }

    IEnumerator AutoSpawnMissile()
    {
        yield return new WaitForSeconds(1.5f);
        while(currentMissileCount_ < maxMissileCount_)
        {
            yield return new WaitForSeconds(missileSpawnInterval_);

            if (!buildingManager_.HasBuilding)
                yield break;

            SpawnMissile();
        }
    }

    public void OnGameReStarted(int stageLevel)
    {
        OnGameEnded(false, 0);
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        if (missiles_.Count == 0)
            return;

        for (int i = 0; i < missiles_.Count; ++i)
        {
            UnBindEvents(missiles_[i]);
            int index = missiles_.IndexOf(missiles_[i]);
            missileFactory_.Restore(missiles_[i]);
            missiles_.RemoveAt(index);
            i -= 1;
        }
    }

    public void OnStageUp(int stageLevel)
    {
        currentMissileCount_ = 0;
        stageLevel_ = stageLevel;
        maxMissileCount_ = stageLevel * 2;
        missileSpawnInterval_ = 2f / stageLevel;
        if (stageLevel % 10 != 0 && stageLevel < endStageLevel_)
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
            SortingOrderSetting(leftMissileRecycle);
            Vector3 spawnMissile = GetMissileSpawnPosition(transforms[0].position.x);
            leftMissileRecycle.Activate(spawnMissile, transforms[0].position);
            BossPatternDoubleAttackSetting(leftMissileRecycle);

            RecycleObject rightMissileRecycle = missileFactory_.Get();
            SortingOrderSetting(rightMissileRecycle);
            spawnMissile = GetMissileSpawnPosition(transforms[3].position.x);
            rightMissileRecycle.Activate(spawnMissile, transforms[3].position);
            BossPatternDoubleAttackSetting(rightMissileRecycle);
        }
        else
        {
            RecycleObject leftMissileRecycle = missileFactory_.Get();
            SortingOrderSetting(leftMissileRecycle);
            Vector3 spawnMissile = GetMissileSpawnPosition(transforms[1].position.x);
            leftMissileRecycle.Activate(spawnMissile, transforms[1].position);
            BossPatternDoubleAttackSetting(leftMissileRecycle);

            RecycleObject rightMissileRecycle = missileFactory_.Get();
            SortingOrderSetting(rightMissileRecycle);
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
        currentMissileCount_++;
    }

    public void OnBossCircleAttack(int maxCount, Vector3 position)
    {
        int count = 0;
        int maxCountNum = UnityEngine.Random.Range(maxCount, maxCount + 2);
        while (count <= maxCountNum)
        {
            RecycleObject missileRecycle = missileFactory_.Get();
            SortingOrderSetting(missileRecycle);
            missileRecycle.transform.rotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * count / maxCountNum;
            missileRecycle.transform.Rotate(rotVec);
            missileRecycle.Activate(position);

            BindEvents(missileRecycle);

            missiles_.Add(missileRecycle);
            currentMissileCount_++;
            count += 1;
        }
    }

    public void OnBossPatternFanShapeAttack(float count, int maxCount, Vector3 position)
    {
        RecycleObject missileRecycle = missileFactory_.Get();
        SortingOrderSetting(missileRecycle);
        missileRecycle.transform.rotation = Quaternion.identity;

        float z = Mathf.Abs(Mathf.Cos(count / maxCount * 10f)) * 120 + 120;

        Vector3 rotVec = Vector3.forward * z;
        missileRecycle.transform.Rotate(rotVec);
        missileRecycle.Activate(position);

        BindEvents(missileRecycle);
        missiles_.Add(missileRecycle);
        currentMissileCount_++;
    }
}
