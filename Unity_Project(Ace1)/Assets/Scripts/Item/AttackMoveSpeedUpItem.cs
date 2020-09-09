using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class AttackMoveSpeedUpItem : Item
{
    protected int maxItemScore_ = 500;

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
        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        bottomY_ = position.y - box_.size.y;

        position = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        topY_ = position.y + box_.size.y;

        position = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        leftX_ = position.x - box_.size.x;

        position = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        rightX_ = position.x + box_.size.x;
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
            if(!isFirstExplosion_)
            {
                isFirstExplosion_ = true;
                return;
            }

            isActivated_ = false;
            isFirstExplosion_ = false;
            Destroyed?.Invoke(this);
        }
    }
}