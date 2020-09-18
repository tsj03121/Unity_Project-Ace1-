using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    [SerializeField]
    Shape shape_;

    [SerializeField]
    GridManager gridManager_;

    [SerializeField]
    List<GameObject> currBlocks_;

    [SerializeField]
    List<GameObject> downPosBlocks_;

    [SerializeField]
    List<ShapeType> shapeTypes_;

    [SerializeField]
    Material[] materials;

    ShapeType nextShapeType = ShapeType.NULL;

    Factory blockFactory_;
    [SerializeField]
    Factory downPosShowFactory_;
    BlockConfirm blockConfirm;
    Controller controller_;

    public Action<ShapeType> CreateBlockAction;

    // Start is called before the first frame update
    void Awake()
    {
        blockConfirm = new BlockConfirm();
    }

    void Start()
    {
        NewCreateShape();
    }

    void BindEvents()
    {
        controller_.KeyInputAction += shape_.OnKeyInput;
        shape_.NextPosDownAction += gridManager_.isNextPosDown;
        shape_.NextPosMoveAction += gridManager_.isNextPosMove;
        shape_.NextRotMoveAction += gridManager_.isNextRotMove;
        shape_.BlocksGridInputAction += gridManager_.CurrBlocksGridInput;
    }

    void ShapeTypeSetting()
    {
        shapeTypes_.Add(ShapeType.I);
        shapeTypes_.Add(ShapeType.O);
        shapeTypes_.Add(ShapeType.J);
        shapeTypes_.Add(ShapeType.L);
        shapeTypes_.Add(ShapeType.S);
        shapeTypes_.Add(ShapeType.Z);
        shapeTypes_.Add(ShapeType.T);
    }

    public void Init(Controller controller, Factory blockFactory, Factory downShowFactory)
    {
        blockFactory_ = blockFactory;
        controller_ = controller;
        downPosShowFactory_ = downShowFactory;
        BindEvents();
    }

    public void OnBlocksInput()
    {
        currBlocks_.RemoveRange(0, currBlocks_.Count);

        for(int i = 0; i < downPosBlocks_.Count; ++i)
        {
            downPosShowFactory_.Restore(downPosBlocks_[i]);
        }
        downPosBlocks_.RemoveRange(0, downPosBlocks_.Count);

        NewCreateShape();
    }

    public void NewCreateShape(int size = 4)
    {
        if (shapeTypes_.Count == 0)
        {
            ShapeTypeSetting();
        }

        ShapeType shapeType;
        int randomNum;

        if (nextShapeType == ShapeType.NULL)
        {
            randomNum = UnityEngine.Random.Range(0, shapeTypes_.Count);
            shapeType = shapeTypes_[randomNum];
            shapeTypes_.RemoveAt(randomNum);
        }
        else
        {
            shapeType = nextShapeType;
        }

        randomNum = UnityEngine.Random.Range(0, shapeTypes_.Count);
        nextShapeType = shapeTypes_[randomNum];
        shapeTypes_.RemoveAt(randomNum);

        Vector3[] vector3s = blockConfirm.GetShapePos_(shapeType);

        for (int i = 0; i < size; ++i)
        {
            currBlocks_.Add(blockFactory_.Get());
            downPosBlocks_.Add(downPosShowFactory_.Get());
            int materialsIndex = (int) shapeType;
            currBlocks_[i].GetComponent<MeshRenderer>().material = materials[materialsIndex];
            currBlocks_[i].transform.position = vector3s[i];
            downPosBlocks_[i].transform.position = vector3s[i];
        }

        shape_.Setting(currBlocks_, downPosBlocks_, shapeType);
        CreateBlockAction?.Invoke(nextShapeType);
    }

    public void OnRestart()
    {
        NewCreateShape();
    }

    public void OnGameOver()
    {
        shapeTypes_.RemoveRange(0, shapeTypes_.Count);
        nextShapeType = ShapeType.NULL;

        for (int i = 0; i < currBlocks_.Count; ++i)
        {
            blockFactory_.Restore(currBlocks_[i]);
            downPosShowFactory_.Restore(downPosBlocks_[i]);
        }        
        currBlocks_.RemoveRange(0, currBlocks_.Count);
        downPosBlocks_.RemoveRange(0, downPosBlocks_.Count);
    }
}
