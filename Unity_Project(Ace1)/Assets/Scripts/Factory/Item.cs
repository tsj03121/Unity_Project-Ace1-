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
}
