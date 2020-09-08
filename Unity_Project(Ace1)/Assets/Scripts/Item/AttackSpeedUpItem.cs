using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class AttackSpeedUpItem : Item
{
    protected int maxItemScore_ = 1000;

    public override int GetMaxItemScore() { return maxItemScore_; }

    void Awake()
    {
        box_ = GetComponent<BoxCollider2D>();
        body_ = GetComponent<Rigidbody2D>();

        body_.bodyType = RigidbodyType2D.Kinematic;
        box_.isTrigger = true;
    }

    void Start()
    {
        Vector3 bottomPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        bottomY_ = bottomPosition.y - box_.size.y;
    }

    void Update()
    {
        transform.position += transform.up * moveSpeed_ * Time.deltaTime;
        CheckOutOfScreen();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explosion>() != null)
        {
            if (!isFirstExplosion_)
            {
                isFirstExplosion_ = true;
                return;
            }

            isFirstExplosion_ = false;
            Destroyed?.Invoke(this);
        }
    }

    void CheckOutOfScreen()
    {
        if (transform.position.y < bottomY_)
        {
            isActivated_ = false;
            OutOfScreen?.Invoke(this);
        }
    }
}
