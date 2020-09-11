using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BulletLauncher launcherPrefab_;
    BulletLauncher launcher_;

    [SerializeField]
    Transform launcherLocator_;

    [SerializeField]
    Building buildingPrefab_;

    [SerializeField]
    Transform[] buildingLocators_;

    [SerializeField]
    Missile missilePrefab_;

    [SerializeField]
    DestroyEffect effectPrefab_;

    [SerializeField]
    AttackSpeedUpItem attackSpeedItemPrefab_;

    [SerializeField]
    AttackMoveSpeedUpItem attackMoveSpeedUpItemrefab_;

    [SerializeField]
    HpUpItem hpUpItemPrefab_;

    [SerializeField]
    Boss bossPrefab_;

    [SerializeField]
    int maxMissileCount_ = 2;

    [SerializeField]
    float missileSpawnInterval_ = 2f;

    [SerializeField]
    int scorePerMissile_ = 50;

    [SerializeField]
    int scorePerBuilding_ = 5000;

    [SerializeField]
    int endStageLevel_;

    [SerializeField]
    UIRoot uIRoot_;
    UIAnimation uIAnimation_;


    bool isAllBuildingDestroyed_ = false;

    public Action<bool, int> GameEnded;

    MouseGameController mouseGameController_;
    BuildingManager buildingManager_;
    TimeManager timeManager_;
    MissileManager missileManager_;
    ScoreManager scoreManager_;
    StageManager stageManager_;
    ItemManager itemManager_;
    BossManager bossManager_;

    // Start is called before the first frame update
    void Start()
    {
        launcher_ = Instantiate(launcherPrefab_);
        launcher_.transform.position = launcherLocator_.position;

        mouseGameController_ = gameObject.AddComponent<MouseGameController>();
        buildingManager_ = new BuildingManager(new Factory<Building>(buildingPrefab_), buildingLocators_, new Factory<DestroyEffect>(effectPrefab_, 2));
        timeManager_ = gameObject.AddComponent<TimeManager>();
        missileManager_ = gameObject.AddComponent<MissileManager>();
        missileManager_.Initialize(new Factory<RecycleObject>(missilePrefab_), buildingManager_, maxMissileCount_, missileSpawnInterval_, endStageLevel_);
        stageManager_ = new StageManager(buildingManager_, endStageLevel_);
        scoreManager_ = new ScoreManager(scorePerMissile_, scorePerBuilding_);
        itemManager_ = new ItemManager(launcher_, buildingManager_,
            new Factory<Item>(attackSpeedItemPrefab_), new Factory<Item>(attackMoveSpeedUpItemrefab_), new Factory<Item>(hpUpItemPrefab_));
        bossManager_ = gameObject.AddComponent<BossManager>();
        bossManager_.Initialize(bossPrefab_, missileManager_, launcher_.transform);
        uIAnimation_ = uIRoot_.GetUIAnimation();
        uIAnimation_.SetEndStageLevel(endStageLevel_);

        BindEvents();

        timeManager_.StartGame(1f);
    }

    void BindEvents()
    {
        mouseGameController_.FireButtonPressed += launcher_.OnFireButtonPressed;

        uIRoot_.ReStart += scoreManager_.OnGameReStarted;
        uIRoot_.ReStart += buildingManager_.OnGameReStarted;
        uIRoot_.ReStart += stageManager_.OnGameReStarted;
        uIRoot_.ReStart += launcher_.OnGameReStarted;
        uIRoot_.ReStart += missileManager_.OnGameReStarted;
        uIRoot_.ReStart += OnGameReStarted;

        timeManager_.GameStarted += buildingManager_.OnGameStarted;
        timeManager_.GameStarted += launcher_.OnGameStarted;
        timeManager_.GameStarted += missileManager_.OnGameStarted;
        timeManager_.GameStarted += uIRoot_.OnGameStarted;

        missileManager_.MissileDestroyed += scoreManager_.OnMissileDestroyed;
        missileManager_.MissileDestroyed += itemManager_.OnMissileDestroyed;
        missileManager_.AllMissilesDestroyed += stageManager_.OnAllMissilesDestroyed;

        scoreManager_.ScoreChanged += uIRoot_.OnScoreChanged;

        buildingManager_.AllBuildingsDestroyed += OnAllBuildingDestroyed;
        buildingManager_.AddBuildingScore += scoreManager_.OnAddBuildingScore;

        stageManager_.StageUp += missileManager_.OnStageUp;
        stageManager_.StageUp += uIRoot_.OnStageUp;
        stageManager_.StageUp += uIAnimation_.OnStageLevelTextMove;
        stageManager_.StageUp += bossManager_.OnStageUp;
        stageManager_.EndedStage += OnEndedStage;

        launcher_.MaxItem += scoreManager_.OnMaxItem;

        bossManager_.BossClear += stageManager_.OnBossClear;
        bossManager_.BossClear += scoreManager_.OnBossClearScore;

        GameEnded += launcher_.OnGameEnded;
        GameEnded += missileManager_.OnGameEnded;
        GameEnded += scoreManager_.OnGameEnded;
        GameEnded += uIRoot_.OnGameEnded;
        GameEnded += itemManager_.OnGameEnded;
        GameEnded += bossManager_.OnGameEnded;
    }

    void UnBindEvents()
    {
        mouseGameController_.FireButtonPressed -= launcher_.OnFireButtonPressed;

        uIRoot_.ReStart -= scoreManager_.OnGameReStarted;
        uIRoot_.ReStart -= buildingManager_.OnGameReStarted;
        uIRoot_.ReStart -= stageManager_.OnGameReStarted;
        uIRoot_.ReStart -= launcher_.OnGameReStarted;
        uIRoot_.ReStart -= missileManager_.OnGameReStarted;
        uIRoot_.ReStart -= OnGameReStarted;

        timeManager_.GameStarted -= buildingManager_.OnGameStarted;
        timeManager_.GameStarted -= launcher_.OnGameStarted;
        timeManager_.GameStarted -= missileManager_.OnGameStarted;
        timeManager_.GameStarted -= uIRoot_.OnGameStarted;

        missileManager_.MissileDestroyed -= scoreManager_.OnMissileDestroyed;
        missileManager_.MissileDestroyed -= itemManager_.OnMissileDestroyed;
        missileManager_.AllMissilesDestroyed -= stageManager_.OnAllMissilesDestroyed;

        scoreManager_.ScoreChanged -= uIRoot_.OnScoreChanged;

        buildingManager_.AllBuildingsDestroyed -= OnAllBuildingDestroyed;

        stageManager_.StageUp -= missileManager_.OnStageUp;
        stageManager_.StageUp -= uIRoot_.OnStageUp;
        stageManager_.StageUp -= uIAnimation_.OnStageLevelTextMove;
        stageManager_.StageUp -= bossManager_.OnStageUp;
        stageManager_.EndedStage -= OnEndedStage;

        launcher_.MaxItem -= scoreManager_.OnMaxItem;

        bossManager_.BossClear -= stageManager_.OnBossClear;
        bossManager_.BossClear -= scoreManager_.OnBossClearScore;

        GameEnded -= launcher_.OnGameEnded;
        GameEnded -= missileManager_.OnGameEnded;
        GameEnded -= scoreManager_.OnGameEnded;
        GameEnded -= uIRoot_.OnGameEnded;
        GameEnded -= itemManager_.OnGameEnded;
        GameEnded -= bossManager_.OnGameEnded;
    }

    void OnDestroy()
    {
        UnBindEvents();    
    }

    void OnGameReStarted(int stageLevel)
    {
        isAllBuildingDestroyed_ = false;
    }

    void OnAllBuildingDestroyed()
    {
        isAllBuildingDestroyed_ = true;
        GameEnded?.Invoke(false, buildingManager_.GetBuildingCount);

        AudioManager.instance_.PlaySound(SoundId.GameEnd);
    }

    void OnEndedStage()
    {
        if(stageManager_.GetStageLevel() == endStageLevel_)
        {
            StartCoroutine(DelayedGameEnded());
        } 
    }

    IEnumerator DelayedGameEnded()
    {
        yield return null;

        if (!isAllBuildingDestroyed_)
        {
            GameEnded?.Invoke(true, buildingManager_.GetBuildingCount);
            AudioManager.instance_.PlaySound(SoundId.GameEnd);
        }
    }
}
