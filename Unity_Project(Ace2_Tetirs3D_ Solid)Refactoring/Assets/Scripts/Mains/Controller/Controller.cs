using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField]
    GameObject _gridViewGameObject;

    public enum KEY_CODE
    {
        X_ROT = 0,
        X_ROT_RERVERS,
        Y_ROT,
        Y_ROT_RERVERSE,
        Z_ROT,
        Z_ROT_RERVERSE,
        FORWARD,
        RIGHT,
        BACK,
        LEFT,
        DROPDOWN,
        LEFT_CONTROL_DOWN,
        LEFT_CONTROL_UP
    }

    public Action<KEY_CODE> CallbackKeyInput;

    bool isGameOver = false;
    bool _isMouseDrag = false;
    Vector3 _mousePos;
    
    void Update()
    {
        if (isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.Z_ROT);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.Z_ROT_RERVERSE);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.Y_ROT);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.Y_ROT_RERVERSE);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.X_ROT);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.X_ROT_RERVERS);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            KEY_CODE modifyKey = (KEY_CODE) GetCameraRotKeyValueChange(KEY_CODE.FORWARD);
            CallbackKeyInput?.Invoke(modifyKey);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            KEY_CODE modifyKey = (KEY_CODE) GetCameraRotKeyValueChange(KEY_CODE.BACK);
            CallbackKeyInput?.Invoke(modifyKey);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            KEY_CODE modifyKey = (KEY_CODE) GetCameraRotKeyValueChange(KEY_CODE.LEFT);
            CallbackKeyInput?.Invoke(modifyKey);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            KEY_CODE modifyKey = (KEY_CODE) GetCameraRotKeyValueChange(KEY_CODE.RIGHT);
            CallbackKeyInput?.Invoke(modifyKey);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.DROPDOWN);
        }

        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.LEFT_CONTROL_DOWN);
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            CallbackKeyInput?.Invoke(KEY_CODE.LEFT_CONTROL_UP);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            _mousePos = Input.mousePosition;
            _isMouseDrag = true;
        }

        else if(Input.GetMouseButtonUp(0))
        {
            _isMouseDrag = false;
        }

        if (_isMouseDrag)
        {
            Vector3 dir = Input.mousePosition - _mousePos;
            float x = dir.y * Time.deltaTime + _gridViewGameObject.transform.eulerAngles.x;
            float y = dir.x * Time.deltaTime + _gridViewGameObject.transform.eulerAngles.y;

            _gridViewGameObject.transform.eulerAngles = new Vector3(x, y, 0);
        }
    }

    int GetCameraRotKeyValueChange(KEY_CODE keyCode)
    {
        float cameraRot = _gridViewGameObject.transform.eulerAngles.y % 315;

        if (cameraRot < 45)
        {
            int keyValue = (int)keyCode;
            return keyValue;
        }
        else if (cameraRot <= 315 && cameraRot >= 225)
        {
            int keyValue = (int)keyCode + 3;
            if (keyValue > 9)
            {
                keyValue -= 4;
            }
            return keyValue;
        }
        else if (cameraRot < 225 && cameraRot >= 135)
        {
            int keyValue = (int)keyCode + 2;
            if (keyValue > 9)
            {
                keyValue -= 4;
            }
            return keyValue;
        }
        else if (cameraRot < 135 && cameraRot >= 45)
        {
            int keyValue = (int)keyCode + 1;
            if(keyValue > 9)
            {
                keyValue -= 4;
            }
            return keyValue;
        }
        return 0;
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }

    public void OnReStart()
    {
        isGameOver = false;
    }
}
