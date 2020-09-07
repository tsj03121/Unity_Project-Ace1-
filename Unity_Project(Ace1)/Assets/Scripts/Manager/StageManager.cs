using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager
{
    [SerializeField]
    int stageLevel = 1;

    public Action<int> StageUp;

    public int GetStageLevel() { return stageLevel; }

    public void OnAllMissilesDestroyed()
    {
        stageLevel += 1;
        StageUp?.Invoke(stageLevel);
    }
}
