using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory
{
    List<Building> pool_ = new List<Building>();
    int defaultPoolSize_;
    Building prefab_;

    public BuildingFactory(Building prefab, int defaultPoolSize = 4)
    {
        prefab_ = prefab;
        defaultPoolSize_ = defaultPoolSize;

        Debug.Assert(prefab_ != null, "Prefab is null!");
    }

    void CreatePool()
    {
        for (int i = 0; i < defaultPoolSize_; ++i)
        {
            Building obj = GameObject.Instantiate(prefab_) as Building;
            obj.gameObject.SetActive(false);
            pool_.Add(obj);
        }
    }

    public Building Get()
    {
        if (pool_.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = pool_.Count - 1;
        Building obj = pool_[lastIndex];
        pool_.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(Building obj)
    {
        Debug.Assert(obj != null, "Null object to be returned!");
        obj.gameObject.SetActive(false);
        pool_.Add(obj);
    }
}
