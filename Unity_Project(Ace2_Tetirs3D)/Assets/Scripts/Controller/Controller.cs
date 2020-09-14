using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum KEY_CODE
{
    UP_ARROW = 0,
    DOWN_ARROW = 1,
    LEFT_ARROW = 2,
    RIGHT_ARROW = 3,
    SPACE_BAR = 4
}

public class Controller : MonoBehaviour
{
    // Update is called once per frame

    public Action<KEY_CODE> KeyInput;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            KeyInput?.Invoke(KEY_CODE.UP_ARROW);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            KeyInput?.Invoke(KEY_CODE.DOWN_ARROW);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            KeyInput?.Invoke(KEY_CODE.LEFT_ARROW);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            KeyInput?.Invoke(KEY_CODE.RIGHT_ARROW);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            KeyInput?.Invoke(KEY_CODE.SPACE_BAR);
        }
    }
}
