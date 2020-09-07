using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileManager : MonoBehaviour
{
    Factory missileFactory;
    BuildingManager buildingManager;

    bool isInitialized = false;

    int maxMissileCount = 2;
    int currentMissileCount;

    float missileSpawnInterval = 2f;

    Coroutine spawningMissile;

    List<RecycleObject> missiles = new List<RecycleObject>();

    public Action MissileDestroyed;
    public Action AllMissilesDestroyed;

    public void Initialize(Factory missileFactory, BuildingManager buildingManager, int maxMissileCount, float missileSpawnInterval)
    {
        if (isInitialized)
            return;

        this.missileFactory = missileFactory;
        this.buildingManager = buildingManager;
        this.maxMissileCount = maxMissileCount;
        this.missileSpawnInterval = missileSpawnInterval;

        isInitialized = true;
    }

    void SpawnMissile()
    {
        Debug.Assert(this.missileFactory != null, "missile factory is null!");
        Debug.Assert(this.buildingManager != null, "building Manager is null!");

        RecycleObject missile = missileFactory.Get();
        missile.Activate(GetMissileSpawnPosition(), buildingManager.GetRandomBuildingPosition());
        missile.Destroyed += OnMissileDestroyed;
        missile.OutOfScreen += OnMissileOutOfScreen;
        missiles.Add(missile);

        currentMissileCount++;
    }

    void OnMissileDestroyed(RecycleObject missile)
    {
        RestoreMissile(missile);
        MissileDestroyed?.Invoke();
    }

    void OnMissileOutOfScreen(RecycleObject missile)
    {
        RestoreMissile(missile);
    }

    void RestoreMissile(RecycleObject missile)
    {
        missile.Destroyed -= OnMissileDestroyed;
        int index = missiles.IndexOf(missile);
        missiles.RemoveAt(index);
        missileFactory.Restore(missile);

        CheckAllMissileRestored();
    }

    void CheckAllMissileRestored()
    {
        if (currentMissileCount == maxMissileCount && missiles.Count == 0)
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

    public void OnGameStarted()
    {
        currentMissileCount = 0;
        spawningMissile = StartCoroutine(AutoSpawnMissile());
    }

    IEnumerator AutoSpawnMissile()
    {
        while(currentMissileCount < maxMissileCount)
        {
            yield return new WaitForSeconds(missileSpawnInterval);

            if (!buildingManager.HasBuilding)
                yield break;

            SpawnMissile();
        }
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        if (missiles.Count == 0)
            return;

        foreach (var missile in missiles)
        {
            missileFactory.Restore(missile);
        }
    }

    public void OnGameStageUp(int stageLevle)
    {
        StopCoroutine(spawningMissile);
        currentMissileCount = 0;
        maxMissileCount = stageLevle * 2;
        missileSpawnInterval = 2f / stageLevle;
        spawningMissile = StartCoroutine(AutoSpawnMissile());
        Debug.Log("Stage count : " + maxMissileCount + ", missleinterval : " + missileSpawnInterval);
    }
}
