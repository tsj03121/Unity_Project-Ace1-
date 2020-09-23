using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shape : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _currBlocks;
    List<GameObject> _downPosBlocks;

    public enum RotType
    {
        X_ROT,
        Y_ROT,
        Z_ROT
    }

    public Coroutine _downCoroutine;

    public Func<List<GameObject>, bool> CallbackNextPosDown;
    public Func<List<GameObject>, Vector3, bool> CallbackNextPosMove;
    public Func<Vector3[], bool> CallbackNextRotMove;
    public Action<List<GameObject>> CallbackBlocksGridInput;

    ShapeConfirm _shapeConfirm;

    IEnumerator Down(float dropSpeed)
    {
        yield return new WaitForSeconds(dropSpeed);
        bool isDown = true;
        while (isDown)
        {
            isDown = (bool) CallbackNextPosDown?.Invoke(_currBlocks);

            NextPos(isDown);

            yield return new WaitForSeconds(dropSpeed);
        }
    }

    void NextPos(bool isDown)
    {
        if (isDown)
        {
            for (int i = 0; i < _currBlocks.Count; ++i)
            {
                _currBlocks[i].transform.position += Vector3.down;
            }
        }
        else
        {
            CallbackBlocksGridInput?.Invoke(_currBlocks);
        }
    }

    void PosMove(Vector3 vector3)
    {
        bool isMove = (bool) CallbackNextPosMove?.Invoke(_currBlocks, vector3);

        if (isMove)
        {
            for (int i = 0; i < _currBlocks.Count; ++i)
            {
                _currBlocks[i].transform.position += vector3;
            }
            SetDownPosBlocks();
        }
    }

    void RotMove(int value, RotType rotType)
    {
        Vector3[] vector3s;
        if (value == 1)
        {
            vector3s = _shapeConfirm.GetShapeRot(_currBlocks, rotType);
        }
        else
        {
            vector3s = _shapeConfirm.GetShapeRotReverse(_currBlocks, rotType);
        }

        bool isRot = (bool) CallbackNextRotMove?.Invoke(vector3s);

        if (isRot)
        {
            for (int i = 0; i < _currBlocks.Count; ++i)
            {
                _currBlocks[i].transform.position = vector3s[i];
            }
            SetDownPosBlocks();
        }
    }

    void SetDownPosBlocks()
    {
        ResetDownPosBlocks();
        bool isDown = true;
        while (isDown)
        {
            isDown = (bool)CallbackNextPosDown?.Invoke(_downPosBlocks);
            for (int i = 0; i < _downPosBlocks.Count; ++i)
            {
                _downPosBlocks[i].transform.position += Vector3.down;
            }
        }
        for (int i = 0; i < _downPosBlocks.Count; ++i)
        {
            _downPosBlocks[i].transform.position += Vector3.up;
        }
    }

    void ResetDownPosBlocks()
    {
        for (int i = 0; i < _downPosBlocks.Count; ++i)
        {
            _downPosBlocks[i].transform.position = _currBlocks[i].transform.position;
        }
    }

    public void Setting(List<GameObject> blocks, List<GameObject> downPosBlocks, ShapeConfirm shapeConfirm)
    {
        _currBlocks = blocks;
        _downPosBlocks = downPosBlocks;
        _shapeConfirm = shapeConfirm;
        SetDownPosBlocks();
        _downCoroutine = StartCoroutine(Down(5f));
    }

    public void OnKeyInput(Controller.KEY_CODE key_code)
    {
        switch (key_code)
        {
            //KeyCode.Alpha7 || KeyCode.Keypad7
            case (Controller.KEY_CODE.X_ROT):
                {
                    RotMove(1, RotType.X_ROT);
                    break;
                }

            //KeyCode.Alpha8 || KeyCode.Keypad8
            case (Controller.KEY_CODE.X_ROT_RERVERS):
                {
                    RotMove(-1, RotType.X_ROT);
                    break;
                }

            //KeyCode.Alpha4 || KeyCode.Keypad4
            case (Controller.KEY_CODE.Y_ROT):
                {
                    RotMove(1, RotType.Y_ROT);
                    break;
                }

            //KeyCode.Alpha5 || KeyCode.Keypad5
            case (Controller.KEY_CODE.Y_ROT_RERVERSE):
                {
                    RotMove(-1, RotType.Y_ROT);
                    break;
                }

            //KeyCode.Alpha1 || KeyCode.Keypad1
            case (Controller.KEY_CODE.Z_ROT):
                {
                    RotMove(1, RotType.Z_ROT);
                    break;
                }

            //KeyCode.Alpha2 || KeyCode.Keypad2
            case (Controller.KEY_CODE.Z_ROT_RERVERSE):
                {
                    RotMove(-1, RotType.Z_ROT);
                    break;
                }

            case (Controller.KEY_CODE.LEFT):
                {
                    PosMove(Vector3.left);
                    break;
                }

            case (Controller.KEY_CODE.RIGHT):
                {
                    PosMove(Vector3.right);
                    break;
                }

            case (Controller.KEY_CODE.FORWARD):
                {
                    PosMove(Vector3.forward);
                    break;
                }

            case (Controller.KEY_CODE.BACK):
                {
                    PosMove(Vector3.back);
                    break;
                }

            case (Controller.KEY_CODE.DROPDOWN):
                {
                    StopCoroutine(_downCoroutine);

                    bool isDown = true;
                    while (isDown)
                    {
                        isDown = (bool)CallbackNextPosDown?.Invoke(_currBlocks);
                        NextPos(isDown);
                    }
                    break;
                }

            case (Controller.KEY_CODE.LEFT_CONTROL_DOWN):
                {
                    StopCoroutine(_downCoroutine);
                    _downCoroutine = StartCoroutine(Down(0.1f));
                    break;
                }

            case (Controller.KEY_CODE.LEFT_CONTROL_UP):
                {
                    StopCoroutine(_downCoroutine);
                    _downCoroutine = StartCoroutine(Down(5f));
                    break;
                }
        }
    }
}
