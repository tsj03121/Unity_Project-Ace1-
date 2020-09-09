using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : RecycleObject
{
    [SerializeField]
    float effectTime_ = 0.5f;
    float elapsedTime_ = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!isActivated_)
            return;

        elapsedTime_ += Time.deltaTime;
        if (elapsedTime_ >= effectTime_)
        {
            elapsedTime_ = 0;
            isActivated_ = false;

            Destroyed?.Invoke(this);
        }
    }
}
