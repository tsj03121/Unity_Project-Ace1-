using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager
{
    [SerializeField]
    int stageLevel_ = 9;

    public Action<int> StageUp;
    public Action EndedStage;

    public int GetStageLevel() { return stageLevel_; }

    public void OnAllMissilesDestroyed()
    {
        AddStageLevel();
    }

    public void OnBossClear(int num)
    {
        AddStageLevel();
    }

    void AddStageLevel()
    {
        stageLevel_ += 1;
        StageUp?.Invoke(stageLevel_);
        if (stageLevel_ == 11)
        {
            EndedStage?.Invoke();
        }
    }
}
