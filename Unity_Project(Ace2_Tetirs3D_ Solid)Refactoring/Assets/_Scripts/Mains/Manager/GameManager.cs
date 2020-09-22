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
        _gridManager.CallbackBlocksInput += _cameraManager.OnBlocksInput;
        _gridManager.CallbackBlocksInput += _shapeManager.OnBlocksInput;
        _gridManager.CallbackLineClear += _scoreManager.OnLineClear;

        _shapeManager.CallbackCreateBlock += _cameraManager.OnCreateBlock;

        _scoreManager.CallbackScoreUpdate += _uIManager.OnScoreUpdate;

        _gridManager.CallbackGameOver += _cameraManager.OnGameOver;
        _gridManager.CallbackGameOver += _uIManager.OnGameOver;
        _gridManager.CallbackGameOver += _shapeManager.OnGameOver;
        _gridManager.CallbackGameOver += _controller.OnGameOver;

        _uIManager.CallbackReStart += _shapeManager.OnReStart;
        _uIManager.CallbackReStart += _scoreManager.OnReStart;
        _uIManager.CallbackReStart += _controller.OnReStart;
    }

}
