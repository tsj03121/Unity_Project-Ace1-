using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    List<Item> pool_ = new List<Item>();
    int defaultPoolSize_;
    Item prefab_;

    public ItemFactory(Item prefab, int defaultPoolSize = 3)
    {
        prefab_ = prefab;
        defaultPoolSize_ = defaultPoolSize;

        Debug.Assert(prefab_ != null, "Prefab is null!");
    }

    void CreatePool()
    {
        for (int i = 0; i < defaultPoolSize_; ++i)
        {
            Item obj = GameObject.Instantiate(prefab_) as Item;
            obj.gameObject.SetActive(false);
            pool_.Add(obj);
        }
    }

    public Item Get()
    {
        if (pool_.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = pool_.Count - 1;
        Item obj = pool_[lastIndex];
        pool_.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(Item obj)
    {
        Debug.Assert(obj != null, "Null object to be returned!");
        obj.gameObject.SetActive(false);
        pool_.Add(obj);
    }
}
