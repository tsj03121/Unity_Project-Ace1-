using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class AttackSpeedItem : Item
{
    [SerializeField]
    float moveSpeed = 5;

    public Action<Item> AttackSpeedUpItem;

    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explosion>() != null)
        {
            AttackSpeedUpItem?.Invoke(this);
        }
    }

}
