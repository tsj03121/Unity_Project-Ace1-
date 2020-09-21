using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    [SerializeField]
    Shape _shape;

    [SerializeField]
    GridManager _gridManager;

    [SerializeField]
    Material[] _materials;

    public Action<ShapeConfirm.ShapeType> CallbackCreateBlock;

    ShapeConfirm.ShapeType _nextShapeType = ShapeConfirm.ShapeType.NULL;
    Factory _blockFactory;
    Factory _downPosShowFactory;
    ShapeConfirm _blockConfirm;
    Controller _controller;

    List<GameObject> _downPosBlocks = new List<GameObject>();
    List<GameObject> _currBlocks = new List<GameObject>();
    List<ShapeConfirm.ShapeType> _shapeTypes = new List<ShapeConfirm.ShapeType>();

    [SerializeField]
    ShapeConfirm shapeConfirm = new ShapeConfirm();

    // Start is called before the first frame update
    void Awake()
    {
        _blockConfirm = new ShapeConfirm();
    }

    void Start()
    {
        NewCreateShape();
    }

    void BindEvents()
    {
        _controller.CallbackKeyInput += _shape.OnKeyInput;
        _shape.CallbackNextPosDown += _gridManager.OnNextPosDown;
        _shape.CallbackNextPosMove += _gridManager.OnNextPosMove;
        _shape.CallbackNextRotMove += _gridManager.OnNextRotMove;
        _shape.CallbackBlocksGridInput += _gridManager.OnBlocksGridInput;
    }

    void ShapeTypeSetting()
    {
        for(int i = 0; i < (int) ShapeConfirm.ShapeType.NULL; i++)
        {
            _shapeTypes.Add((ShapeConfirm.ShapeType)i);
        }
    }

    public void Init(Controller controller, Factory blockFactory, Factory downShowFactory)
    {
        _blockFactory = blockFactory;
        _controller = controller;
        _downPosShowFactory = downShowFactory;
        BindEvents();
    }

    public void OnBlocksInput()
    {
        _currBlocks.RemoveRange(0, _currBlocks.Count);

        for(int i = 0; i < _downPosBlocks.Count; ++i)
        {
            _downPosShowFactory.Restore(_downPosBlocks[i]);
        }
        _downPosBlocks.RemoveRange(0, _downPosBlocks.Count);

        NewCreateShape();
    }

    public void NewCreateShape()
    {
        if (_shapeTypes.Count == 0)
        {
            ShapeTypeSetting();
        }

        ShapeConfirm.ShapeType shapeType;
        int randomNum;

        if (_nextShapeType == ShapeConfirm.ShapeType.NULL)
        {
            randomNum = UnityEngine.Random.Range(0, _shapeTypes.Count);
            shapeType = _shapeTypes[randomNum];
            _shapeTypes.RemoveAt(randomNum);
        }
        else
        {
            shapeType = _nextShapeType;
        }

        randomNum = UnityEngine.Random.Range(0, _shapeTypes.Count);
        _nextShapeType = _shapeTypes[randomNum];
        _shapeTypes.RemoveAt(randomNum);

        Vector3[] vector3s = _blockConfirm.GetShapePos(shapeType);

        for (int i = 0; i < vector3s.Length; ++i)
        {
            _currBlocks.Add(_blockFactory.Get());
            _downPosBlocks.Add(_downPosShowFactory.Get());

            int materialsIndex = (int) shapeType;
            _currBlocks[i].GetComponent<MeshRenderer>().material = _materials[materialsIndex];

            _currBlocks[i].transform.position = vector3s[i];
            _downPosBlocks[i].transform.position = vector3s[i];
        }

        _shape.Setting(_currBlocks, _downPosBlocks);
        CallbackCreateBlock?.Invoke(_nextShapeType);
    }

    public void OnReStart()
    {
        NewCreateShape();
    }

    public void OnGameOver()
    {
        _shapeTypes.RemoveRange(0, _shapeTypes.Count);
        _nextShapeType = ShapeConfirm.ShapeType.NULL;

        for (int i = 0; i < _currBlocks.Count; ++i)
        {
            _blockFactory.Restore(_currBlocks[i]);
            _downPosShowFactory.Restore(_downPosBlocks[i]);
        }        
        _currBlocks.RemoveRange(0, _currBlocks.Count);
        _downPosBlocks.RemoveRange(0, _downPosBlocks.Count);
    }
}
