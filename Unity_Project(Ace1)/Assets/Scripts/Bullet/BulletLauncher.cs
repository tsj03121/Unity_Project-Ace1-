using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab_;

    [SerializeField]
    Explosion explosionPrefab_;

    [SerializeField]
    Transform firePosition_;

    [SerializeField]
    float bulletMoveSpeedChange_ = 5;
    float fireDelay_ = 0.5f;
    float elapsedFireTime_;
    bool canShoot_ = true;

    bool isGameStarted_ = false;

    Factory bulletFactory_;
    Factory explosionFactory_;

    public Action<int> MaxItem;

    void Start()
    {
        bulletFactory_ = new Factory(bulletPrefab_);
        explosionFactory_ = new Factory(explosionPrefab_);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted_)
            return;

        if (!canShoot_)
        {
            elapsedFireTime_ += Time.deltaTime;
            if (elapsedFireTime_ >= fireDelay_)
            {
                canShoot_ = true;
                elapsedFireTime_ = 0f;
            }
        }
    }

    public void OnFireButtonPressed(Vector3 position)
    {
        if (!isGameStarted_)
            return;

        if (!canShoot_)
            return;

        RecycleObject bulletRecycle = bulletFactory_.Get();
        bulletRecycle.Activate(firePosition_.position, position);
        bulletRecycle.Destroyed += OnBulletDestroyed;

        Bullet bullet = (Bullet)bulletRecycle;
        bullet.SetMoveSpeed(bulletMoveSpeedChange_);
     
        AudioManager.instance_.PlaySound(SoundId.Shoot);

        canShoot_ = false;
    }

    void OnBulletDestroyed(RecycleObject usedBullet)
    {
        Vector3 lastBulletPosition = usedBullet.transform.position;
        usedBullet.Destroyed -= OnBulletDestroyed;
        bulletFactory_.Restore(usedBullet);

        RecycleObject explosion = explosionFactory_.Get();
        explosion.Activate(lastBulletPosition);
        explosion.Destroyed += OnExplosionDestroyed;

        AudioManager.instance_.PlaySound(SoundId.BulletExplosion);
    }

    void OnExplosionDestroyed(RecycleObject usedExplosion)
    {
        usedExplosion.Destroyed -= OnExplosionDestroyed;
        explosionFactory_.Restore(usedExplosion);
    }

    public void OnGameReStarted(int stageLevel)
    {
        isGameStarted_ = true;
        fireDelay_ = 0.5f;
        bulletMoveSpeedChange_ = 5;
    }

    public void OnGameStarted()
    {
        isGameStarted_ = true;
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        isGameStarted_ = false;
    }

    public void OnItemDestroyed(Item item)
    {
        switch (item.GetType().ToString())
        {
            case "AttackSpeedUpItem":
            {
                if (fireDelay_ <= 0.1f)
                {
                    int itemScore = item.GetMaxItemScore();
                    MaxItem?.Invoke(itemScore);
                    return;
                }
                 
                fireDelay_ -= 0.1f;
                break;
            }

            case "AttackMoveSpeedUpItem":
            {
                if (bulletMoveSpeedChange_ >= 10)
                {
                    int itemScore = item.GetMaxItemScore();
                    MaxItem?.Invoke(itemScore);
                    return;
                }

                bulletMoveSpeedChange_ += 1;
                break;
            }
        }
    }
}
