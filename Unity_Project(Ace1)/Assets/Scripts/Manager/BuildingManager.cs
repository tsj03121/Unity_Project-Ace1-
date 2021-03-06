﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingManager
{
    Transform[] buildingLocators_;
    Factory<DestroyEffect> effectFactory_;
    Factory<Building> buildingFactory_;

    List<Building> buildings_ = new List<Building>();

    public int GetBuildingCount { get { return buildings_.Count; } }
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

    public BuildingManager(Factory<Building> buildingFactory, Transform[] buildingLocators, Factory<DestroyEffect> effectFactory)
    {
        buildingLocators_ = buildingLocators;
        effectFactory_ = effectFactory;
        buildingFactory_ = buildingFactory;
    }

    void CreateBuildings()
    {
        if (buildings_.Count > 0)
        {
            for (int i = 0; i < buildings_.Count; ++i)
            {
                UnBindEvents(buildings_[i]);
                int index = buildings_.IndexOf(buildings_[i]);
                buildingFactory_.Restore(buildings_[i]);
                buildings_.RemoveAt(index);
                i -= 1;
            }
        }

        for (int i = 0; i < buildingLocators_.Length; ++i)
        {
            Building building = buildingFactory_.Get();
            building.transform.position = buildingLocators_[i].position;
            BindEvents(building);
            buildings_.Add(building);
        }
    }

    void BindEvents(Building building)
    {
        building.Destroyed += OnBuildingDestroyed;
    }

    void UnBindEvents(Building building)
    {
        building.Destroyed -= OnBuildingDestroyed;
    }

    void OnBuildingDestroyed(Building building)
    {
        AudioManager.instance_.PlaySound(SoundId.BuildingExplosion);

        Vector3 lastPosition = building.transform.position;
        UnBindEvents(building);
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
        effectFactory_.Restore(effect as DestroyEffect);
    }

    public void OnGameStarted()
    {
        CreateBuildings();
    }

    public void OnGameReStarted(int stageLevel)
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
                if (GetBuildingCount == 4)
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
