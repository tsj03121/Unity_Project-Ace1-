using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected BoxCollider2D box_;
    protected Rigidbody2D body_;

    [SerializeField]
    protected float moveSpeed_ = 2;

    protected float bottomY_;
    protected float topY_;
    protected float leftX_;
    protected float rightX_;

    protected bool isActivated_ = false;
    protected bool isFirstExplosion_ = false;

    public Action<Item> Destroyed;
    public Action<Item> OutOfScreen;

    public void SetIsFirstExplosion() { isFirstExplosion_ = true; }
    public virtual int GetMaxItemScore() { return 0; }

    public virtual void Activate(RecycleObject missile)
    {
        isActivated_ = true;
        transform.position = missile.transform.position;
        transform.rotation = missile.transform.rotation;
    }

    public void CheckOutOfScreen()
    {
        if (isActivated_)
        {
            if (transform.position.y < bottomY_)
            {
                isActivated_ = false;
                OutOfScreen?.Invoke(this);
                return;
            }

            if (transform.position.y > topY_)
            {
                isActivated_ = false;
                OutOfScreen?.Invoke(this);
                return;
            }

            if (transform.position.x < leftX_)
            {
                isActivated_ = false;
                OutOfScreen?.Invoke(this);
                return;
            }

            if (transform.position.x > rightX_)
            {
                isActivated_ = false;
                OutOfScreen?.Invoke(this);
                return;
            }
        }
    }
}
