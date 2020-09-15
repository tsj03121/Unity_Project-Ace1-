﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ShapeType
{
    I,
    O,
    T,
    S,
    Z,
    L,
    J,
    NULL
}

public enum RotType
{
    X_ROT,
    Y_ROT,
    Z_ROT
}

public class Shape : MonoBehaviour
{
    [SerializeField]
    List<GameObject> currBlocks_;

    BlockConfirm blockConfirm_;

    Coroutine downCoroutine;

    ShapeType currShapeType;

    int zRotCount_ = 0;
    int yRotCount_ = 0;
    int xRotCount_ = 0;

    [SerializeField]
    float dropSpeed = 0.5f;

    public Func<List<GameObject>, bool> NextPosDownAction;
    public Func<List<GameObject>, Vector3, bool> NextPosMoveAction;
    public Func<List<GameObject>, Vector3[], int, bool> NextRotMoveAction;
    public Action<List<GameObject>> BlocksGridInputAction;

    void Start()
    {
        blockConfirm_ = new BlockConfirm();
    }

    IEnumerator Down()
    {
        bool isDown = true;
        while (isDown)
        {
            isDown = (bool) NextPosDownAction?.Invoke(currBlocks_);

            NextPos(isDown);

            yield return new WaitForSeconds(dropSpeed);
        }
    }

    void NextPos(bool isDown)
    {
        if (isDown)
        {
            for (int i = 0; i < currBlocks_.Count; ++i)
            {
                currBlocks_[i].transform.position += Vector3.down;
            }
        }
        else
        {
            BlocksGridInputAction?.Invoke(currBlocks_);
        }
    }

    void PosMove(Vector3 vector3)
    {
        bool isMove = (bool) NextPosMoveAction?.Invoke(currBlocks_, vector3);

        if (isMove)
        {
            for(int i = 0; i < currBlocks_.Count; ++i)
            {
                currBlocks_[i].transform.position += vector3;
            }
        }
    }

    void RotMove(int value, RotType rotType)
    {
        Vector3[] vector3s = blockConfirm_.GetShapeRot_(currBlocks_, currShapeType, zRotCount_, yRotCount_, xRotCount_, rotType);
        bool isRot = (bool) NextRotMoveAction?.Invoke(currBlocks_, vector3s, value);

        if (!isRot)
        {
            zRotCount_ -= value;
            return;
        }
            
        if (isRot)
        {
            for (int i = 0; i < currBlocks_.Count; ++i)
            {
                currBlocks_[i].transform.position += (vector3s[i] * value);
            }
        }
    }

    public void Setting(List<GameObject> blocks, ShapeType shapeType)
    {
        currBlocks_ = blocks;
        currShapeType = shapeType;
        zRotCount_ = 0;
        downCoroutine = StartCoroutine(Down());
    }

    public void OnKeyInput(KEY_CODE key_code)
    {
        switch (key_code)
        {
            case (KEY_CODE.KEY_INPUI_Z_ROT):
                {
                    zRotCount_ += 1;
                    zRotCount_ = zRotCount_ % 4;
                    RotMove(1, RotType.Z_ROT);
                    break;
                }
            case (KEY_CODE.KEY_INPUI_Z_ROT_RERVERSE):
                {
                    RotMove(-1, RotType.Z_ROT);
                    zRotCount_ -= 1;
                    if (zRotCount_ < 0)
                    {
                        zRotCount_ = 3;
                    }
                    break;
                }
            case (KEY_CODE.KEY_INPUI_X_ROT):
                {
                    xRotCount_ += 1;
                    xRotCount_ = xRotCount_ % 4;
                    RotMove(1, RotType.X_ROT);
                    break;
                }
            case (KEY_CODE.KEY_INPUI_X_ROT_RERVERSE):
                {
                    RotMove(-1, RotType.X_ROT);
                    xRotCount_ -= 1;
                    if (xRotCount_ < 0)
                    {
                        xRotCount_ = 3;
                    }
                    break;
                }
            case (KEY_CODE.KEY_INPUI_Y_ROT):
                {
                    yRotCount_ += 1;
                    yRotCount_ = yRotCount_ % 4;
                    RotMove(1, RotType.Y_ROT);
                    break;
                }
            case (KEY_CODE.KEY_INPUI_Y_ROT_RERVERSE):
                {
                    RotMove(-1, RotType.Y_ROT);
                    yRotCount_ -= 1;
                    if (yRotCount_ < 0)
                    {
                        yRotCount_ = 3;
                    }
                    break;
                }
            case (KEY_CODE.KEY_INPUT_X_LEFT):
                {
                    PosMove(Vector3.left);
                    break;
                }
            case (KEY_CODE.KEY_INPUT_X_RIGHT):
                {
                    PosMove(Vector3.right);
                    break;
                }
            case (KEY_CODE.KEY_INPUT_X_DROPDOWN):
                {
                    StopCoroutine(downCoroutine);

                    bool isDown = true;
                    while (isDown)
                    {
                        isDown = (bool)NextPosDownAction?.Invoke(currBlocks_);
                        NextPos(isDown);
                    }
                    break;
                }
            case (KEY_CODE.KEY_INPUT_X_FORWARD):
                {
                    PosMove(Vector3.forward);
                    break;
                }
            case (KEY_CODE.KEY_INPUT_X_BACK):
                {
                    PosMove(Vector3.back);
                    break;
                }


        }
    }
}
