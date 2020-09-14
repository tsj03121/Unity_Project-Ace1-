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
    List<ShapeType> shapeTypes_;

    [SerializeField]
    Material[] materials;

    ShapeType nextShapeType = ShapeType.NULL;

    Factory blockFactory_;
    BlockConfirm blockConfirm;
    Controller controller_;

    public Action<ShapeType> CreateBlock;

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
        controller_.KeyInput += shape_.OnKeyInput;
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

    public void Init(Controller controller, Factory blockFactory)
    {
        blockFactory_ = blockFactory;
        controller_ = controller;
        BindEvents();
    }

    public void OnBlocksInput()
    {
        currBlocks_.RemoveRange(0, currBlocks_.Count);
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
            int materialsIndex = (int) shapeType;
            currBlocks_[i].GetComponent<MeshRenderer>().material = materials[materialsIndex];
            currBlocks_[i].transform.position = vector3s[i];
        }

        shape_.Setting(currBlocks_, gridManager_, shapeType);
        CreateBlock?.Invoke(nextShapeType);
    }

    public void OnRestart()
    {
        NewCreateShape();
    }

    public void OnGameOver()
    {
        for(int i = 0; i < currBlocks_.Count; ++i)
        {
            blockFactory_.Restore(currBlocks_[i]);
        }
    }
}
