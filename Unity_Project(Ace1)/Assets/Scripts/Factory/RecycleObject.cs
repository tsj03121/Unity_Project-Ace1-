using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecycleObject : MonoBehaviour
{
    protected bool isActivated_ = false;
    protected Vector3 targetPosition_;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    public Action<RecycleObject> Destroyed;
    public Action<RecycleObject> OutOfScreen;

    public SpriteRenderer GetSpriteRenderer() { return spriteRenderer; }

    public virtual void Activate(Vector3 position)
    {
        isActivated_ = true;
        transform.position = position;
    }
    public virtual void Activate(Vector3 startPosition, Vector3 targetPosition)
    {
        transform.position = startPosition;
        this.targetPosition_ = targetPosition;
        Vector3 dir = (targetPosition - startPosition).normalized;
        transform.rotation = Quaternion.LookRotation(transform.forward, dir);
        isActivated_ = true;
    }
}
