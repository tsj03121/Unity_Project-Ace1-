using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : RecycleObject
{
    [SerializeField]
    float effectTime = 0.5f;
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
            return;

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= effectTime)
        {
            elapsedTime = 0;
            isActivated = false;

            Destroyed?.Invoke(this);
        }
    }
}
