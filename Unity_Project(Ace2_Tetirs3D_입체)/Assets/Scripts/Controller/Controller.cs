using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum KEY_CODE
{
    KEY_INPUI_X_ROT = 0,
    KEY_INPUI_X_ROT_RERVERSE,
    KEY_INPUI_Y_ROT,
    KEY_INPUI_Y_ROT_RERVERSE,
    KEY_INPUI_Z_ROT,
    KEY_INPUI_Z_ROT_RERVERSE,
    KEY_INPUT_X_LEFT,
    KEY_INPUT_X_RIGHT,
    KEY_INPUT_X_DROPDOWN,
    KEY_INPUT_X_FORWARD,
    KEY_INPUT_X_BACK
}

public class Controller : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;

    bool isMouseDrag = false;
    Vector3 mousePos;
    // Update is called once per frame

    public Action<KEY_CODE> KeyInputAction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUI_X_ROT);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUI_X_ROT_RERVERSE);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUT_X_LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUT_X_RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUT_X_DROPDOWN);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUT_X_FORWARD);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUT_X_BACK);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUI_Y_ROT);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUI_Y_ROT_RERVERSE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUI_Z_ROT);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            KeyInputAction?.Invoke(KEY_CODE.KEY_INPUI_Z_ROT_RERVERSE);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            isMouseDrag = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isMouseDrag = false;
        }

        if (isMouseDrag)
        {
            Vector3 dir = Input.mousePosition - mousePos;
            float x = dir.y * Time.deltaTime + gameObject.transform.eulerAngles.x;
            float y = dir.x * Time.deltaTime + gameObject.transform.eulerAngles.y;

            gameObject.transform.rotation = Quaternion.Euler(x, y, 0);
        }


    }
}
