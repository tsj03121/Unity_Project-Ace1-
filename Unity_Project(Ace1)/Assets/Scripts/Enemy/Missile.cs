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
        Vector3 bottomPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        bottomY_ = bottomPosition.y - box_.size.y;
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
    }
}
