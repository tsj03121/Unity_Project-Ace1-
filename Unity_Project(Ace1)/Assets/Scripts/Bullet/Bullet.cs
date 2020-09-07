using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : RecycleObject
{
    [SerializeField]
    float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
            return;

        transform.position += transform.up * moveSpeed * Time.deltaTime;

        if(IsArrivedToTarget())
        {
            isActivated = false;

            if(Destroyed != null)
            {
                Destroyed(this);
            }
        }
    }

    bool IsArrivedToTarget()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance < 0.1f;
    }
}
