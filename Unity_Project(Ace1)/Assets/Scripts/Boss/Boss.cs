using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Boss : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D box_;
    Rigidbody2D body_;

    [SerializeField]
    float bossHp_;

    [SerializeField]
    int bossClearScore = 10000;

    [SerializeField]
    Animator animator_;

    public int GetBossClearScore() { return bossClearScore; }
    public void BossHit() { bossHp_ -= 1; }

    public Action<Boss> BossClear;

    void Awake()
    {
        box_ = GetComponent<BoxCollider2D>();
        body_ = GetComponent<Rigidbody2D>();

        box_.isTrigger = true;
        body_.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.GetComponent<Explosion>() != null)
        {
            BossHit();
            if(bossHp_ == 0)
            {
                BossClear?.Invoke(this);
                return;
            }
            return;
        }
    }

    public void OnBossDoubleAttackAnimation()
    {
        BossRunAwayAnim();
    }

    public void OnBossCircleAttackAnimation(int maxCount, Vector3 position)
    {
        BossRunAwayAnim();
    }

    public void OnBossFanShapeAttackAnimation(float floatNum, int intNum, Vector3 position)
    {
        BossRunAwayAnim();
    }

    void BossRunAwayAnim()
    {
        if (animator_.GetBool("BossRunAwayCheck"))
            return;

        animator_.SetBool("BossRunAwayCheck", true);
        animator_.SetTrigger("BossRunAway");
    }

    public void OnBossAppearAnim()
    {
        animator_.SetBool("BossRunAwayCheck", false);
        animator_.SetTrigger("BossAppear");
    }
}
