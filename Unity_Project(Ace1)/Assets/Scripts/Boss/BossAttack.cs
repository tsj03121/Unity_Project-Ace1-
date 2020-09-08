using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    Transform target_;

    [SerializeField]
    int doubleAttackCount_ = 2;
    int bossPatternCount_ = 2;

    Factory missileFactory_;

    Coroutine bossPartternCoroutine;

    public Action BossPatternDoubleAttack;
    public Action<Transform> BossPatternCircleAttack;

    public void Initialize(MissileManager missileManager, Transform target)
    {
        target_ = target;
        missileFactory_ = missileManager.GetMissileFactory();
    }

    void Start()
    {
        Debug.Log("BossAttackStart 1번시작");
        bossPartternCoroutine = StartCoroutine(BossAttackPattern());
    }


    IEnumerator BossAttackPattern()
    {
        while (gameObject != null)
        {
            Debug.Log("BossAttackPatten 5초 후 반복");
            yield return new WaitForSeconds(1f);
            int randomPateen = UnityEngine.Random.Range(0, bossPatternCount_);

            switch (randomPateen)
            {
                case 0:
                    {
                        StopCoroutine(bossPartternCoroutine);
                        StartCoroutine(DoubleAttack());
                        break;
                    }

                case 1:
                    {
                        StopCoroutine(bossPartternCoroutine);
                        StartCoroutine(CircleAttack());
                        break;
                    }

                case 2:
                    {
                        break;
                    }

            }
        }
    }


    IEnumerator DoubleAttack()
    {
        int count = 0;
        while (count < doubleAttackCount_)
        {
            Debug.Log("DoubleAttack 반복");
            yield return new WaitForSeconds(1f);

            BossPatternDoubleAttack?.Invoke();

            count += 1;
        }

        bossPartternCoroutine = StartCoroutine(BossAttackPattern());
    }

    IEnumerator CircleAttack()
    {
        Debug.Log("CircleAttack 시작");
        yield return new WaitForSeconds(1f);

        BossPatternCircleAttack?.Invoke(gameObject.transform);
        bossPartternCoroutine = StartCoroutine(BossAttackPattern());
    }
}
