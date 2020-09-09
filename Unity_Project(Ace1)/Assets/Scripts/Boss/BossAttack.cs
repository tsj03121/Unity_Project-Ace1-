using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    Transform target_;

    [SerializeField]
    int doubleAttackCount_ = 4;
    int bossPatternCount_ = 3;

    Factory missileFactory_;

    Coroutine bossPartternCoroutine;

    bool readyParttern = true;

    public Action BossPatternDoubleAttack;
    public Action<Transform> BossPatternCircleAttack;
    public Action<float, int> BossPatternFanShapeAttack;

    public void Initialize(MissileManager missileManager, Transform target)
    {
        target_ = target;
        missileFactory_ = missileManager.GetMissileFactory();
    }

    void Start()
    {
        bossPartternCoroutine = StartCoroutine(BossAttackPattern());
    }


    IEnumerator BossAttackPattern()
    {
        while (gameObject != null)
        {
            yield return new WaitForSeconds(1f);
            if (readyParttern)
            {
                int randomPateen = UnityEngine.Random.Range(0, bossPatternCount_);

                switch (randomPateen)
                {
                    case 0:
                        {
                            readyParttern = false;
                            StartCoroutine(DoubleAttack());
                            break;
                        }

                    case 1:
                        {
                            readyParttern = false;
                            StartCoroutine(CircleAttack());
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
        }
    }

    //더블샷 패턴
    IEnumerator DoubleAttack()
    {
        int count = 0;
        while (count < doubleAttackCount_)
        {
            yield return new WaitForSeconds(1f);

            BossPatternDoubleAttack?.Invoke();

            count += 1;

            if (count == doubleAttackCount_)
            {
                readyParttern = true;
            }
        }
    }

    //원형 패턴
    IEnumerator CircleAttack()
    {
        yield return new WaitForSeconds(1f);

        BossPatternCircleAttack?.Invoke(gameObject.transform);
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
                BossPatternFanShapeAttack?.Invoke(count, maxCount);
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
