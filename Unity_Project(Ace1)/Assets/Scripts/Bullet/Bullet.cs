using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : RecycleObject
{
    [SerializeField]
    float moveSpeed_;

    public void SetMoveSpeed(float speed) { moveSpeed_ = speed; }
    public float GetMoveSpeed() { return moveSpeed_; }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated_)
            return;

        transform.position += transform.up * moveSpeed_ * Time.deltaTime;

        if(IsArrivedToTarget())
        {
            isActivated_ = false;

            if(Destroyed != null)
            {
                Destroyed(this);
            }
        }
    }

    bool IsArrivedToTarget()
    {
        float distance = Vector3.Distance(transform.position, targetPosition_);
        return distance < 0.1f;
    }
}
