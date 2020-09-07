using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    List<Item> pool = new List<Item>();
    int defaultPoolSize;
    Item prefab;

    public ItemFactory(Item prefab, int defaultPoolSize = 3)
    {
        this.prefab = prefab;
        this.defaultPoolSize = defaultPoolSize;

        Debug.Assert(this.prefab != null, "Prefab is null!");
    }

    void CreatePool()
    {
        for (int i = 0; i < defaultPoolSize; ++i)
        {
            Item obj = GameObject.Instantiate(prefab) as Item;
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public Item Get()
    {
        if (pool.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = pool.Count - 1;
        Item obj = pool[lastIndex];
        pool.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(Item obj)
    {
        Debug.Assert(obj != null, "Null object to be returned!");
        obj.gameObject.SetActive(false);
        pool.Add(obj);
    }
}
