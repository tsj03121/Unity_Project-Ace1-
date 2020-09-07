using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager
{
    readonly int scorePerMissile;
    readonly int scorePerBuilding;

    int score;

    public Action<int> ScoreChanged;

    public ScoreManager(int scorePerMissile = 50, int scorePerBuilding = 5000)
    {
        this.scorePerMissile = scorePerMissile;
        this.scorePerBuilding = scorePerBuilding;
    }

    public void OnMissileDestroyed()
    {
        score += scorePerMissile;
        ScoreChanged?.Invoke(score);
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        if (buildingCount == 0)
            return;

        score += scorePerBuilding * buildingCount;
        ScoreChanged?.Invoke(score);
    }

}
