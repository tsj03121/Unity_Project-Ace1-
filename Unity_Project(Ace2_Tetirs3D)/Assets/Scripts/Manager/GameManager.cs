using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GridManager gridManager_;

    [SerializeField]
    ShapeManager shapeManager_;

    [SerializeField]
    Controller controller_;

    [SerializeField]
    Factory blockFactory_;

    [SerializeField]
    GameObject prefab_;

    [SerializeField]
    CameraManager cameraManager_;

    [SerializeField]
    UIManager uIManager_;

    void Awake()
    {
        blockFactory_ = new Factory(prefab_);
        shapeManager_.Init(controller_, blockFactory_);
        gridManager_.Init(blockFactory_);
        BindEvents();
    }

    void BindEvents()
    {
        gridManager_.BlocksInput += cameraManager_.OnBlocksInput;
        gridManager_.BlocksInput += shapeManager_.OnBlocksInput;

        shapeManager_.CreateBlock += cameraManager_.OnCreateBlock;

        gridManager_.GameOver += uIManager_.OnGameOver;
        gridManager_.GameOver += cameraManager_.OnGameOver;
        gridManager_.GameOver += shapeManager_.OnGameOver;

        uIManager_.ReStart += gridManager_.OnReStart;
        uIManager_.ReStart += shapeManager_.OnRestart;

    }

}
