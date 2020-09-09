using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossManager : MonoBehaviour
{
    Boss boss;
    BossAttack bossAttack_;
    MissileManager missileManager_;
    Transform target_;

    public Action<int> BossClear;

    public void Initialize(Boss prefab, MissileManager missileManager, Transform target)
    {
        boss = prefab;
        missileManager_ = missileManager;
        target_ = target;
    }

    void BindEvents()
    {
        bossAttack_.BossPatternDoubleAttack += missileManager_.OnBossPatternDoubleAttack;
        bossAttack_.BossPatternCircleAttack += missileManager_.OnBossCircleAttack;
        bossAttack_.BossPatternFanShapeAttack += missileManager_.OnBossPatternFanShapeAttack;

        boss.BossClear += OnBossClear;
    }

    void UnBindEvents()
    {
        bossAttack_.BossPatternDoubleAttack -= missileManager_.OnBossPatternDoubleAttack;
        bossAttack_.BossPatternCircleAttack -= missileManager_.OnBossCircleAttack;
        bossAttack_.BossPatternFanShapeAttack -= missileManager_.OnBossPatternFanShapeAttack;

        boss.BossClear -= OnBossClear;
    }

    void BossCreate()
    {
        boss = GameObject.Instantiate(boss) as Boss;
        bossAttack_ = boss.gameObject.AddComponent<BossAttack>();
        bossAttack_.Initialize(missileManager_, target_);

        BindEvents();
    }

    void OnBossClear(Boss boss)
    {
        UnBindEvents();
        BossClear?.Invoke(boss.GetBossClearScore());

        Destroy(boss.gameObject);        
    }

    public void OnStageUp(int stage)
    {
        if (stage == 10)
        {
            BossCreate();
        }
    }
}
