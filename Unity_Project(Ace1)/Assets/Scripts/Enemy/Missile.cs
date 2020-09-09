using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Missile : RecycleObject
{
    BoxCollider2D box_;
    Rigidbody2D body_;

    [SerializeField]
    float moveSpeed_ = 3f;

    float bottomY_;
    float topY_;
    float leftX_;
    float rightX_;

    public Action<RecycleObject> BuildingDestroyed;

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

    void DestroySelf()
    {
        isActivated_ = false;
        Destroyed?.Invoke(this);
    }

    void BuildingDestroyedSelf()
    {
        isActivated_ = false;
        BuildingDestroyed?.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated_)
            return;

        transform.position += transform.up * moveSpeed_ * Time.deltaTime;
        CheckOutOfScreen();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Building>() != null)
        {
            BuildingDestroyedSelf();
            return;
        }

        if (collision.GetComponent<Explosion>() != null)
        {
            DestroySelf();
            return;
        }
    }

    void CheckOutOfScreen()
    {
        if (transform.position.y < bottomY_)
        {
            isActivated_ = false;
            OutOfScreen?.Invoke(this);
        }

        if (transform.position.y > topY_)
        {
            isActivated_ = false;
            OutOfScreen?.Invoke(this);
        }

        if (transform.position.x < leftX_)
        {
            isActivated_ = false;
            OutOfScreen?.Invoke(this);
        }

        if (transform.position.x > rightX_)
        {
            isActivated_ = false;
            OutOfScreen?.Invoke(this);
        }
    }
}
