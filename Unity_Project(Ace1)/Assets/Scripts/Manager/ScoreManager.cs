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
        Debug.Log("Score 증가! " + scorePerBuilding_);
        score_ += scorePerBuilding_;
    }

    public void OnMaxItem(int score)
    {
        Debug.Log("Score 증가! " + score);
        score_ += score;
    }

    public void OnBossClearScore(int score)
    {
        Debug.Log("Score 증가! : " + score);
        score_ += score;
    }

}
