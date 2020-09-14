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

    [SerializeField]
    GridManager gridManager_;

    BlockConfirm blockConfirm_;

    ShapeType currShapeType;

    int rotCount_ = 0;

    void Start()
    {
        blockConfirm_ = new BlockConfirm();
    }

    IEnumerator Down()
    {
        bool isDown = true;
        while (isDown)
        {
            isDown = gridManager_.isNextPosDown(currBlocks_);

            if (isDown)
            {
                for (int i = 0; i < currBlocks_.Count; ++i)
                {
                    currBlocks_[i].transform.position += Vector3.down;
                }
            }
            else
            {
                gridManager_.CurrBlocksGridInput(currBlocks_);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    void PosMove(Vector3 vector3)
    {
        bool isMove = gridManager_.isNextPosMove(currBlocks_, vector3);

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
        bool isRot = gridManager_.isNextRotMove(currBlocks_, vector3s, value);

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

    public void Setting(List<GameObject> blocks, GridManager gridManager, ShapeType shapeType)
    {
        currBlocks_ = blocks;
        gridManager_ = gridManager;
        currShapeType = shapeType;
        rotCount_ = 0;
        StartCoroutine(Down());
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
                    break;
                }
        }
    }
}
