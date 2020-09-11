using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shape : MonoBehaviour
{
    [SerializeField]
    Block[] blocks = null;

    Transform BeforeTransform_;

    bool isTrigger_;

    void Start()
    {
        BindEvents();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveRotation(90);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveRotation(-90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePosition(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePosition(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void BindEvents()
    {
        for (int i = 0; i < blocks.Length; ++i)
        {
             blocks[i].ShapeTriggerSetting += IsShapeTriggerSetting;
        }
    }

    void UnBindEvents()
    {
        for (int i = 0; i < blocks.Length; ++i)
        {
            blocks[i].ShapeTriggerSetting -= IsShapeTriggerSetting;
        }
    }

    void MoveRotation(int num)
    {
        BeforeTransform_ = transform;
        transform.Rotate(0, 0, num);
    }

    void MovePosition(Vector3 vector3)
    {
        BeforeTransform_ = transform;
        transform.position += vector3;
    }

    void IsShapeTriggerSetting(Block block, bool isTrigger)
    {
        isTrigger_ = isTrigger;
        if (isTrigger_)
        {
            ModifyPosition(block);
            return;
        }

        isTrigger_ = isTrigger;
    }

    void ModifyPosition(Block block)
    {
        if (isTrigger_)
        {
            float x = block.transform.position.x;

            if (x > 9)
            {
                x = x - 9;
                transform.position += Vector3.left * x;
            }
            else if (x < 0)
            {
                x = 0 - x;
                transform.position += Vector3.right * x;
            }
            else
            {
                transform.position = BeforeTransform_.position;
                transform.rotation = BeforeTransform_.rotation;
            }
        }
    }
}
