using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager
{
    readonly int scorePerMissile_;
    readonly int scorePerBuilding_;

    int score_;

    public Action<int> ScoreChanged;

    public ScoreManager(int scorePerMissile = 50, int scorePerBuilding = 5000)
    {
        scorePerMissile_ = scorePerMissile;
        scorePerBuilding_ = scorePerBuilding;
    }

    public void OnMissileDestroyed(RecycleObject missile)
    {
        score_ += scorePerMissile_;
        ScoreChanged?.Invoke(score_);
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        if (buildingCount == 0)
            return;

        score_ += scorePerBuilding_ * buildingCount;
        ScoreChanged?.Invoke(score_);
    }

    public void OnAddBuildingScore()
    {
        score_ += scorePerBuilding_;
        ScoreChanged?.Invoke(score_);
    }

    public void OnMaxItem(int score)
    {
        score_ += score;
        ScoreChanged?.Invoke(score_);
    }

    public void OnBossClearScore(int score)
    {
        score_ += score;
        ScoreChanged?.Invoke(score_);
    }

    public void OnGameReStarted(int stageLevel)
    {
        score_ = 0;
        ScoreChanged?.Invoke(score_);
    }

}
