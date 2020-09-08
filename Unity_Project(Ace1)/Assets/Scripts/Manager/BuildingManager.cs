using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingManager
{
    Transform[] buildingLocators_;
    Factory effectFactory_;
    BuildingFactory buildingFactory_;

    List<Building> buildings_ = new List<Building>();

    public int BuildingCount { get { return buildings_.Count; } }
    public Transform[] GetBuildingLocators() { return buildingLocators_; }

    public bool HasBuilding
    {
        get
        {
            return buildings_.Count > 0;
        }
    }

    public Action AllBuildingsDestroyed;
    public Action AddBuildingScore;

    public BuildingManager(BuildingFactory buildingFactory, Transform[] buildingLocators, Factory effectFactory)
    {
        buildingLocators_ = buildingLocators;
        effectFactory_ = effectFactory;
        buildingFactory_ = buildingFactory;

        Debug.Assert(buildingLocators_ != null, "null buildingLocators!");
    }

    void CreateBuildings()
    {
        if (buildings_.Count > 0)
        {
            Debug.LogWarning("Buildings have been already Created!");
            return;
        }

        for (int i = 0; i < buildingLocators_.Length; ++i)
        {
            Building building = buildingFactory_.Get();
            building.transform.position = buildingLocators_[i].position;
            building.Destroyed += OnBuildingDestroyed;
            buildings_.Add(building);
        }

    }

    void OnBuildingDestroyed(Building building)
    {
        AudioManager.instance_.PlaySound(SoundId.BuildingExplosion);

        Vector3 lastPosition = building.transform.position;
        building.Destroyed -= OnBuildingDestroyed;
        int index = buildings_.IndexOf(building);
        buildings_.RemoveAt(index);
        buildingFactory_.Restore(building);

        RecycleObject effect = effectFactory_.Get();
        effect.Activate(lastPosition);
        effect.Destroyed += OnEffectDestroyed;

        if (buildings_.Count == 0)
        {
            AllBuildingsDestroyed?.Invoke();
        }
    }

    void OnEffectDestroyed(RecycleObject effect)
    {
        effect.Destroyed -= OnEffectDestroyed;
        effectFactory_.Restore(effect);
    }

    public void OnGameStarted()
    {
        CreateBuildings();
    }

    public Vector3 GetRandomBuildingPosition()
    {
        Building building = buildings_[UnityEngine.Random.Range(0, buildings_.Count)];
        return building.transform.position;
    }

    public void OnItemDestroyed(Item item)
    {
        switch (item.GetType().ToString())
        {
            case "HpUpItem":
            {
                Debug.Log(buildings_.Count);
                if (BuildingCount == 4)
                {
                    AddBuildingScore?.Invoke();
                    break;
                }

                Building building = buildingFactory_.Get();
                building.Destroyed += OnBuildingDestroyed;
                buildings_.Add(building);
                break;
            }
        }
    }
}
