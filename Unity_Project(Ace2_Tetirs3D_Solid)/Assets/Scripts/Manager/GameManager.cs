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
    ScoreManager scoreManager_;

    [SerializeField]
    CameraManager cameraManager_;

    [SerializeField]
    Controller controller_;

    [SerializeField]
    Factory blockFactory_;

    [SerializeField]
    GameObject prefab_;

    [SerializeField]
    GameObject downPosShowPrefab_;

    [SerializeField]
    UIManager uIManager_;

    void Awake()
    {
        blockFactory_ = new Factory(prefab_);
        Factory downPosShowFactory_ = new Factory(downPosShowPrefab_, 4);
        shapeManager_.Init(controller_, blockFactory_, downPosShowFactory_);
        gridManager_.Init(blockFactory_);
        BindEvents();
    }

    void BindEvents()
    {
        gridManager_.BlocksInputAction += cameraManager_.OnBlocksInput;
        gridManager_.BlocksInputAction += shapeManager_.OnBlocksInput;
        gridManager_.LineClearAction += scoreManager_.OnLineClear;

        shapeManager_.CreateBlockAction += cameraManager_.OnCreateBlock;

        scoreManager_.ScoreUpdateAction += uIManager_.OnScoreUpdate;

        gridManager_.GameOverAction += cameraManager_.OnGameOver;
        gridManager_.GameOverAction += uIManager_.OnGameOver;
        gridManager_.GameOverAction += shapeManager_.OnGameOver;

        uIManager_.ReStartAction += shapeManager_.OnRestart;
        uIManager_.ReStartAction += scoreManager_.OnReStart;
    }

}
