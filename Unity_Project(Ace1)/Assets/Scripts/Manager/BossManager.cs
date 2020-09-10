using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossManager : MonoBehaviour
{
    Boss boss;
    Boss bossPrefab_;
    BossAttack bossAttack_;
    MissileManager missileManager_;
    Transform target_;

    public Action<int> BossClear;

    public void Initialize(Boss prefab, MissileManager missileManager, Transform target)
    {
        bossPrefab_ = prefab;
        missileManager_ = missileManager;
        target_ = target;
    }

    void BindEvents()
    {
        bossAttack_.BossPatternDoubleAttack += boss.OnBossDoubleAttackAnimation;
        bossAttack_.BossPatternDoubleAttack += missileManager_.OnBossPatternDoubleAttack;

        bossAttack_.BossPatternCircleAttack += boss.OnBossCircleAttackAnimation;
        bossAttack_.BossPatternCircleAttack += missileManager_.OnBossCircleAttack;

        bossAttack_.BossPatternFanShapeAttack += boss.OnBossFanShapeAttackAnimation;
        bossAttack_.BossPatternFanShapeAttack += missileManager_.OnBossPatternFanShapeAttack;

        bossAttack_.BossAppear += boss.OnBossAppearAnim;

        boss.BossClear += OnBossClear;
    }

    void UnBindEvents()
    {
        bossAttack_.BossPatternDoubleAttack -= boss.OnBossDoubleAttackAnimation;
        bossAttack_.BossPatternDoubleAttack -= missileManager_.OnBossPatternDoubleAttack;

        bossAttack_.BossPatternCircleAttack -= boss.OnBossCircleAttackAnimation;
        bossAttack_.BossPatternCircleAttack -= missileManager_.OnBossCircleAttack;

        bossAttack_.BossPatternFanShapeAttack -= boss.OnBossFanShapeAttackAnimation;
        bossAttack_.BossPatternFanShapeAttack -= missileManager_.OnBossPatternFanShapeAttack;

        bossAttack_.BossAppear -= boss.OnBossAppearAnim;

        boss.BossClear -= OnBossClear;
    }

    void BossCreate()
    {
        boss = GameObject.Instantiate(bossPrefab_) as Boss;
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
        if (stage % 10 == 0)
        {
            Invoke("BossCreate", 1.5f);
        }
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        if (isVictory)
            return;

        if(boss != null)
        {
            Destroy(boss.gameObject);
        }
    }

}
