using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GridManager _gridManager;

    [SerializeField]
    ShapeManager _shapeManager;

    [SerializeField]
    ScoreManager _scoreManager;

    [SerializeField]
    CameraManager _cameraManager;

    [SerializeField]
    Controller _controller;

    [SerializeField]
    Factory _blockFactory;

    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    GameObject _downPosShowPrefab;

    [SerializeField]
    UIManager _uIManager;

    void Awake()
    { 
        _blockFactory = new Factory(_prefab);
        Factory downPosShowFactory_ = new Factory(_downPosShowPrefab, 4);

        _shapeManager.Init(_controller, _blockFactory, downPosShowFactory_);
        _gridManager.Init(_blockFactory);
        BindEvents();
    }

    void BindEvents()
    {
        _shapeManager.CallbackCreateBlock += _cameraManager.OnCreateBlock;

        _scoreManager.CallbackScoreUpdate += _uIManager.OnScoreUpdate;

        _gridManager.CallbackLineClear += _scoreManager.OnLineClear;
        _gridManager.CallbackGameOver += _uIManager.OnGameOver;
        _gridManager.CallbackGameOver += _controller.OnGameOver;
        _gridManager.CallbackShapeInput += _cameraManager.OnShapeInput;
        _gridManager.CallbackGameOver += _cameraManager.OnGameOver;
        _gridManager.CallbackShapeInput += _shapeManager.OnShapeInput;
        _gridManager.CallbackGameOver += _shapeManager.OnGameOver;

        _uIManager.CallbackReStart += _shapeManager.OnReStart;
        _uIManager.CallbackReStart += _scoreManager.OnReStart;
        _uIManager.CallbackReStart += _controller.OnReStart;
    }

}
