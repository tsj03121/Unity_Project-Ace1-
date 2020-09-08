using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossManager : MonoBehaviour
{
    Boss prefab_;
    BossAttack bossAttack_;
    ScoreManager scoreManager_;
    MissileManager missileManager_;
    Transform target_;
    
    public Action BossClear;

    public void Initialize(Boss prefab, ScoreManager scoreManager, MissileManager missileManager, Transform target)
    {
        prefab_ = prefab;
        scoreManager_ = scoreManager;
        missileManager_ = missileManager;
        target_ = target;
    }

    void BossCreate()
    {
        Boss boss = GameObject.Instantiate(prefab_) as Boss;
        bossAttack_ = boss.gameObject.AddComponent<BossAttack>();
        bossAttack_.Initialize(missileManager_, target_);
        bossAttack_.BossPatternDoubleAttack += missileManager_.OnBossPatternDoubleAttack;
        bossAttack_.BossPatternCircleAttack += missileManager_.OnBossCircleAttack;
        boss.BossClear += OnBossClear;
    }

    void OnBossClear(Boss boss)
    {
        bossAttack_.BossPatternDoubleAttack -= missileManager_.OnBossPatternDoubleAttack;
        bossAttack_.BossPatternCircleAttack -= missileManager_.OnBossCircleAttack;
        boss.BossClear -= OnBossClear;
        Destroy(boss.gameObject);
        BossClear?.Invoke();
    }

    public void OnStageUp(int stage)
    {
        if (stage == 10)
        {
            BossCreate();
        }
    }
}
