using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager
{
    ItemFactory attackSpeedUpItemFactory_;
    ItemFactory attackMoveSpeedUpItemFactory_;
    ItemFactory hpUpItemFactory_;
    BulletLauncher bulletLauncher_;
    BuildingManager buildingManager_;

    List<Item> items_ = new List<Item>();


    public ItemManager(BulletLauncher bulletLauncher, BuildingManager buildingManager, ItemFactory attackSpeedUpItemFactory, ItemFactory attackMoveSpeedUpItemFactory, ItemFactory hpUpItemFactory)
    {
        bulletLauncher_ = bulletLauncher;
        buildingManager_ = buildingManager;
        attackSpeedUpItemFactory_ = attackSpeedUpItemFactory;
        attackMoveSpeedUpItemFactory_ = attackMoveSpeedUpItemFactory;
        hpUpItemFactory_ = hpUpItemFactory;
    }

    public void OnMissileDestroyed(RecycleObject missile)
    {
        Item item = GetFactoryItem();
        if (item == null)
            return;

        item.Activate(missile);
        BindEvents(item);

        items_.Add(item);
    }

    void BindEvents(Item item)
    {
        item.Destroyed += OnItemDestroyed;
        item.Destroyed += bulletLauncher_.OnItemDestroyed;
        item.Destroyed += buildingManager_.OnItemDestroyed;

        item.OutOfScreen += OnItemOutOfScreen;
    }

    void UnBindEvents(Item item)
    {
        item.Destroyed -= OnItemDestroyed;
        item.Destroyed -= bulletLauncher_.OnItemDestroyed;
        item.Destroyed -= buildingManager_.OnItemDestroyed;

        item.OutOfScreen -= OnItemOutOfScreen;
    }

    void OnItemDestroyed(Item item)
    {
        RestoreItem(item);
    }

    void OnItemOutOfScreen(Item item)
    {
        RestoreItem(item);
    }

    void RestoreItem(Item item)
    {
        UnBindEvents(item);
        int index = items_.IndexOf(item);

        items_.RemoveAt(index);
        RestoreFactoryItem(item);
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        if (items_.Count == 0)
            return;

        for (int i = 0; i < items_.Count; ++i)
        {
            UnBindEvents(items_[i]);
            int index = items_.IndexOf(items_[i]);
            RestoreFactoryItem(items_[i]);
            items_.RemoveAt(index);
            i -= 1;
        }
    }

    void RestoreFactoryItem(Item item)
    {
        switch(item.GetType().ToString())
        {
            case "AttackSpeedUpItem":
            {
                attackSpeedUpItemFactory_.Restore(item);
                break;
            }

            case "AttackMoveSpeedUpItem":
            {
                attackMoveSpeedUpItemFactory_.Restore(item);
                break;
            }
                
            case "HpUpItem":
            {
                hpUpItemFactory_.Restore(item);
                break;
            }
        }
    }

    //아이템 드랍확률 30% 각각 10%
    Item GetFactoryItem()
    {
        int random = UnityEngine.Random.Range(0, 100) + 1;
        if (random <= 70)
            return null;

        Item item = null;

        if (random >= 71 && random <= 80)
        {
            item = attackSpeedUpItemFactory_.Get();
        }
        else if (random >= 81 && random <= 90)
        {
            item = attackMoveSpeedUpItemFactory_.Get();
        }
        else if (random >= 91 && random <= 100)
        {
            item = hpUpItemFactory_.Get();
        }

        return item;
    }
}
