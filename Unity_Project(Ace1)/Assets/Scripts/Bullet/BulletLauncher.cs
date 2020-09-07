using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;

    [SerializeField]
    Explosion explosionPrefab;

    [SerializeField]
    Transform firePosition;

    [SerializeField]
    float fireDelay = 0.5f;
    float elapsedFireTime;
    bool canShoot = true;

    bool isGameStarted = false;

    Factory bulletFactory;
    Factory explosionFactory;

    void Start()
    {
        bulletFactory = new Factory(bulletPrefab);
        explosionFactory = new Factory(explosionPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
            return;

        if (!canShoot)
        {
            elapsedFireTime += Time.deltaTime;
            if (elapsedFireTime >= fireDelay)
            {
                canShoot = true;
                elapsedFireTime = 0f;
            }
        }
    }

    public void OnFireButtonPressed(Vector3 position)
    {
        if (!isGameStarted)
            return;

        if (!canShoot)
            return;

        RecycleObject bullet = bulletFactory.Get();
        bullet.Activate(firePosition.position, position);
        bullet.Destroyed += OnBulletDestroyed;
      
        AudioManager.instance.PlaySound(SoundId.Shoot);

        canShoot = false;
    }

    void OnBulletDestroyed(RecycleObject usedBullet)
    {
        Vector3 lastBulletPosition = usedBullet.transform.position;
        usedBullet.Destroyed -= OnBulletDestroyed;
        bulletFactory.Restore(usedBullet);

        RecycleObject explosion = explosionFactory.Get();
        explosion.Activate(lastBulletPosition);
        explosion.Destroyed += OnExplosionDestroyed;

        AudioManager.instance.PlaySound(SoundId.BulletExplosion);
    }

    void OnExplosionDestroyed(RecycleObject usedExplosion)
    {
        usedExplosion.Destroyed -= OnExplosionDestroyed;
        explosionFactory.Restore(usedExplosion);
    }

    public void OnGameStarted()
    {
        isGameStarted = true;
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        isGameStarted = false;
    }
}
