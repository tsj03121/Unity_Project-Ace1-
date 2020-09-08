using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Explosion : RecycleObject
{
    CircleCollider2D circle_;
    Rigidbody2D body_;

    [SerializeField]
    float timeToRemove_ = 1f;
    float elapsedTime_ = 0f;

    void Awake()
    {
        circle_ = GetComponent<CircleCollider2D>();
        body_ = GetComponent<Rigidbody2D>();

        circle_.isTrigger = true;
        body_.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated_)
        {
            elapsedTime_ += Time.deltaTime;
            if (elapsedTime_ >= timeToRemove_)
            {
                elapsedTime_ = 0;
                DestroySelf();
            }
        }
    }

    void DestroySelf()
    {
        isActivated_ = false;
        Destroyed?.Invoke(this);
    }
}
