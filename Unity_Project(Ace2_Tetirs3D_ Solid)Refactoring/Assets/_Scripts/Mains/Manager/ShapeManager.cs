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
    List<Material> _materials;

    public Action<int> CallbackCreateBlock;

    int _nextShapeType = -1;
    Factory _blockFactory;
    Factory _downPosShowFactory;
    Controller _controller;

    List<GameObject> _downPosBlocks = new List<GameObject>();
    List<GameObject> _currBlocks = new List<GameObject>();

    [SerializeField]
    List<int> _shapeTypes = new List<int>();

    [SerializeField]
    ShapeConfirm _shapeConfirm;

    Vector3 _basicPos;

    // Start is called before the first frame update
    void Awake()
    {
        DataManager dataManager = DataManager.GetInstance();
        ShapeData shapeData = dataManager.GetShapeData();
        _materials = shapeData.GetShapesMaterials();
        _basicPos = GameManager._basicPos;

        _shapeConfirm = new ShapeConfirm(shapeData.GetShapesPosDatas());
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
        int count = _shapeConfirm.GetShapeTypes().Count;
        for (int i = 0; i < count; i++)
        {
            _shapeTypes.Add(i);
        }
    }

    public void Init(Controller controller, Factory blockFactory, Factory downShowFactory)
    {
        _blockFactory = blockFactory;
        _controller = controller;
        _downPosShowFactory = downShowFactory;
        BindEvents();
    }

    public void OnShapeInput()
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

        int randomNum;
        int shapeIndex;
        if (_nextShapeType == -1)
        {
            randomNum = UnityEngine.Random.Range(0, _shapeTypes.Count);
            shapeIndex = _shapeTypes[randomNum];
            _shapeTypes.RemoveAt(randomNum);
        }
        else
        {
            shapeIndex = _nextShapeType;
        }

        randomNum = UnityEngine.Random.Range(0, _shapeTypes.Count);
        _nextShapeType = _shapeTypes[randomNum];
        _shapeTypes.RemoveAt(randomNum);

        Vector3[] vector3s = _shapeConfirm.GetShapePos(shapeIndex);
        Vector3 basicPos = _basicPos;

        for (int i = 0; i < vector3s.Length; ++i)
        {
            _currBlocks.Add(_blockFactory.Get());
            _downPosBlocks.Add(_downPosShowFactory.Get());

            _currBlocks[i].GetComponent<MeshRenderer>().material = _materials[shapeIndex];

            _currBlocks[i].transform.position = vector3s[i] + basicPos;
            _downPosBlocks[i].transform.position = vector3s[i] + basicPos;
        }

        _shape.Setting(_currBlocks, _downPosBlocks, _shapeConfirm);
        CallbackCreateBlock?.Invoke(_nextShapeType);
    }

    public void OnReStart()
    {
        NewCreateShape();
    }

    public void OnGameOver()
    {
        _shapeTypes.RemoveRange(0, _shapeTypes.Count);
        _nextShapeType = -1;

        for (int i = 0; i < _currBlocks.Count; ++i)
        {
            _blockFactory.Restore(_currBlocks[i]);
            _downPosShowFactory.Restore(_downPosBlocks[i]);
        }        
        _currBlocks.RemoveRange(0, _currBlocks.Count);
        _downPosBlocks.RemoveRange(0, _downPosBlocks.Count);
    }
}
