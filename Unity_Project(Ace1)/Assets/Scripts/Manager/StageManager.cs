using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager
{
    int stageLevel_ = 1;
    int endStageLevel_;

    BuildingManager buildingManager_;

    public Action<int> StageUp;
    public Action EndedStage;

    public StageManager(BuildingManager buildingManager, int endStageLevel)
    {
        buildingManager_ = buildingManager;
        endStageLevel_ = endStageLevel;
    }

    public void SetStageLevel(int stageLevel) { stageLevel_ = stageLevel; }
    public int GetStageLevel() { return stageLevel_; }

    public void OnAllMissilesDestroyed()
    {
        int buildingCount = buildingManager_.GetBuildingCount;
        if (buildingCount > 0)
        {
            AddStageLevel();
        }
    }

    public void OnBossClear(int num)
    {
        AddStageLevel();
    }

    void AddStageLevel()
    {
        stageLevel_ += 1;

        if (stageLevel_ == endStageLevel_)
        {
            EndedStage?.Invoke();
            return;
        }

        StageUp?.Invoke(stageLevel_);
    }

    public void OnGameReStarted(int stageLevel)
    {
        SetStageLevel(stageLevel);
        AddStageLevel();
    }
}
