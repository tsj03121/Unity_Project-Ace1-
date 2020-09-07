using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BulletLauncher launcherPrefab;
    BulletLauncher launcher;

    [SerializeField]
    Transform launcherLocator;

    [SerializeField]
    Building buildingPrefab;

    [SerializeField]
    Transform[] buildingLocators;

    [SerializeField]
    Missile missilePrefab;

    [SerializeField]
    DestroyEffect effectPrefab;

    [SerializeField]
    AttackSpeedItem attackSpeedItemPrefab;

    [SerializeField]
    int maxMissileCount = 2;

    [SerializeField]
    float missileSpawnInterval = 2f;

    [SerializeField]
    int scorePerMissile = 50;

    [SerializeField]
    int scorePerBuilding = 5000;

    [SerializeField]
    UIRoot uIRoot;

    bool isAllBuildingDestroyed = false;

    public Action<bool, int> GameEnded;

    MouseGameController mouseGameController;
    BuildingManager buildingManager;
    TimeManager timeManager;
    MissileManager missileManager;
    ScoreManager scoreManager;
    StageManager stageManager;
    ItemManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        launcher = Instantiate(launcherPrefab);
        launcher.transform.position = launcherLocator.position;

        mouseGameController = gameObject.AddComponent<MouseGameController>();
        buildingManager = new BuildingManager(buildingPrefab, buildingLocators, new Factory(effectPrefab, 2));
        timeManager = gameObject.AddComponent<TimeManager>();
        missileManager = gameObject.AddComponent<MissileManager>();
        stageManager = new StageManager();
        missileManager.Initialize(new Factory(missilePrefab), buildingManager, maxMissileCount, missileSpawnInterval);
        scoreManager = new ScoreManager(scorePerMissile, scorePerBuilding);
        itemManager = new ItemManager(new ItemFactory(attackSpeedItemPrefab));


        BindEvents();

        timeManager.StartGame(1f);
    }

    void BindEvents()
    {
        mouseGameController.FireButtonPressed += launcher.OnFireButtonPressed;
        timeManager.GameStarted += buildingManager.OnGameStarted;
        timeManager.GameStarted += launcher.OnGameStarted;
        timeManager.GameStarted += missileManager.OnGameStarted;
        timeManager.GameStarted += uIRoot.OnGameStarted;
        missileManager.MissileDestroyed += scoreManager.OnMissileDestroyed;
        missileManager.AllMissilesDestroyed += OnAllMissileDestroyed;
        missileManager.AllMissilesDestroyed += stageManager.OnAllMissilesDestroyed;
        scoreManager.ScoreChanged += uIRoot.OnScoreChanged;
        buildingManager.AllBuildingsDestroyed += OnAllBuildingDestroyed;
        stageManager.StageUp += missileManager.OnGameStageUp;
        stageManager.StageUp += uIRoot.OnStageUp;
        


        GameEnded += launcher.OnGameEnded;
        GameEnded += missileManager.OnGameEnded;
        GameEnded += scoreManager.OnGameEnded;
        GameEnded += uIRoot.OnGameEnded;
    }

    void UnBindEvents()
    {
        mouseGameController.FireButtonPressed -= launcher.OnFireButtonPressed;
        timeManager.GameStarted -= buildingManager.OnGameStarted;
        timeManager.GameStarted -= launcher.OnGameStarted;
        timeManager.GameStarted -= missileManager.OnGameStarted;
        timeManager.GameStarted -= uIRoot.OnGameStarted;
        missileManager.MissileDestroyed -= scoreManager.OnMissileDestroyed;
        missileManager.AllMissilesDestroyed -= OnAllMissileDestroyed;
        missileManager.AllMissilesDestroyed -= stageManager.OnAllMissilesDestroyed;
        scoreManager.ScoreChanged -= uIRoot.OnScoreChanged;
        buildingManager.AllBuildingsDestroyed -= OnAllBuildingDestroyed;
        stageManager.StageUp -= missileManager.OnGameStageUp;
        stageManager.StageUp -= uIRoot.OnStageUp;

        GameEnded -= launcher.OnGameEnded;
        GameEnded -= missileManager.OnGameEnded;
        GameEnded -= scoreManager.OnGameEnded;
        GameEnded -= uIRoot.OnGameEnded;
    }

    void OnDestroy()
    {
        UnBindEvents();    
    }

    void OnAllBuildingDestroyed()
    {
        isAllBuildingDestroyed = true;
        GameEnded?.Invoke(false, buildingManager.BuildingCount);

        AudioManager.instance.PlaySound(SoundId.GameEnd);
    }

    void OnAllMissileDestroyed()
    {
        if(stageManager.GetStageLevel() == 11)
        {
            StartCoroutine(DelayedGameEnded());
        } 
    }

    IEnumerator DelayedGameEnded()
    {
        yield return null;

        if (!isAllBuildingDestroyed)
        {
            GameEnded?.Invoke(true, buildingManager.BuildingCount);
            AudioManager.instance.PlaySound(SoundId.GameEnd);
        }
    }
}
