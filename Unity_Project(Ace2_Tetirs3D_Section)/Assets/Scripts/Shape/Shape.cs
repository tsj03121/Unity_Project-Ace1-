using System.Collections;
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

public class Shape : MonoBehaviour
{
    [SerializeField]
    List<GameObject> currBlocks_;

    BlockConfirm blockConfirm_;

    Coroutine downCoroutine;

    ShapeType currShapeType;

    int rotCount_ = 0;

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

    void RotMove(int value = 1)
    {
        Vector3[] vector3s = blockConfirm_.GetShapeRot_(currShapeType, rotCount_);
        bool isRot = (bool) NextRotMoveAction?.Invoke(currBlocks_, vector3s, value);

        if (!isRot)
        {
            rotCount_ -= value;
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
        rotCount_ = 0;
        downCoroutine = StartCoroutine(Down());
    }

    public void OnKeyInput(KEY_CODE key_code)
    {
        switch (key_code)
        {
            case (KEY_CODE.UP_ARROW):
                {
                    rotCount_ += 1;
                    rotCount_ = rotCount_ % 4;
                    RotMove();
                    break;
                }
            case (KEY_CODE.DOWN_ARROW):
                {
                    RotMove(-1);
                    rotCount_ -= 1;
                    if (rotCount_ < 0)
                    {
                        rotCount_ = 3;
                    }
                    break;
                }
            case (KEY_CODE.LEFT_ARROW):
                {
                    PosMove(Vector3.left);
                    break;
                }
            case (KEY_CODE.RIGHT_ARROW):
                {
                    PosMove(Vector3.right);
                    break;
                }
            case (KEY_CODE.SPACE_BAR):
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
        }
    }
}
