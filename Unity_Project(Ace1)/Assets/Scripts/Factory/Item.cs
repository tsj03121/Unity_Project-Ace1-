using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    protected bool isActivated = false;

    public Action<Item> Destroyed;
    public Action<Item> OutOfScreen;
    protected Vector3 targetPosition;

    public virtual void Activate(Vector3 position)
    {
        isActivated = true;
        transform.position = position;
    }
    public virtual void Activate(Vector3 startPosition, Vector3 targetPosition)
    {
        transform.position = startPosition;
        this.targetPosition = targetPosition;
        Vector3 dir = (targetPosition - startPosition).normalized;
        transform.rotation = Quaternion.LookRotation(transform.forward, dir);
        isActivated = true;
    }
}
