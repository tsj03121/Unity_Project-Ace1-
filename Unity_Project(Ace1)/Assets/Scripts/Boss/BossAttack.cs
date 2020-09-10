using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttack : MonoBehaviour
{
    bool readyParttern = true;
    Vector3 attackPos_;

    Transform target_;
    Factory missileFactory_;
    Coroutine bossPartternCoroutine_;

    public Action BossAppear;
    public Action BossPatternDoubleAttack;
    public Action<int, Vector3> BossPatternCircleAttack;
    public Action<float, int, Vector3> BossPatternFanShapeAttack;

    public void Initialize(MissileManager missileManager, Transform target)
    {
        target_ = target;
        missileFactory_ = missileManager.GetMissileFactory();
        attackPos_ = transform.position;
    }

    void Start()
    {
        bossPartternCoroutine_ = StartCoroutine(BossAttackPattern(2));
    }

    IEnumerator BossAttackPattern(int bossPartternCount)
    {
        while (gameObject != null)
        {
            if (readyParttern)
            {
                yield return new WaitForSeconds(3f);
                BossParttern(3);
            }

            yield return null;
            if (readyParttern)
            {
                BossAppear?.Invoke();
            }
        }
    }

    void BossParttern(int bossPartternCount)
    {
        int randomPateen = UnityEngine.Random.Range(0, bossPartternCount);
        switch (randomPateen)
        {
            case 0:
            {
                readyParttern = false;
                StartCoroutine(DoubleAttack(2));
                break;
            }

            case 1:
            {
                readyParttern = false;
                StartCoroutine(CircleAttack(20));
                break;
            }

            case 2:
            {
                readyParttern = false;
                StartCoroutine(FanShapeAttack(40));
                break;
            }
        }
    }

    //더블샷 패턴
    IEnumerator DoubleAttack(int doubleAttackCount)
    {
        int count = 0;
        while (count < doubleAttackCount)
        {
            BossPatternDoubleAttack?.Invoke();

            count += 1;

            yield return new WaitForSeconds(1f);

            if (count == doubleAttackCount)
            {
                readyParttern = true;
            }
        }
    }

    //원형 패턴
    IEnumerator CircleAttack(int maxCount)
    {
        BossPatternCircleAttack?.Invoke(maxCount, attackPos_);

        yield return new WaitForSeconds(1f);

        readyParttern = true;
    }

    //부채꼴 패턴
    IEnumerator FanShapeAttack(int maxCount)
    {
        float count = 0;
        bool isPartternAttack = true;

        while (isPartternAttack)
        {
            yield return new WaitForSeconds(0.1f);

            if (count <= maxCount)
            {
                BossPatternFanShapeAttack?.Invoke(count, maxCount, attackPos_);
                count += 1;
            }
            else
            {
                isPartternAttack = false;
                readyParttern = true;
            }
        }
    }
}
