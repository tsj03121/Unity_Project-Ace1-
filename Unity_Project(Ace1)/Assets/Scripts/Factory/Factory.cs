using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    List<RecycleObject> pool = new List<RecycleObject>();
    int defaultPoolSize;
    RecycleObject prefab;

    public Factory(RecycleObject prefab, int defaultPoolSize = 5)
    {
        this.prefab = prefab;
        this.defaultPoolSize = defaultPoolSize;

        Debug.Assert(this.prefab != null, "Prefab is null!");
    }

    void CreatePool()
    {
        for(int i = 0; i < defaultPoolSize; ++i)
        {
            RecycleObject obj = GameObject.Instantiate(prefab) as RecycleObject;
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public RecycleObject Get()
    {
        if(pool.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = pool.Count - 1;
        RecycleObject obj = pool[lastIndex];
        pool.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(RecycleObject obj)
    {
        Debug.Assert(obj != null, "Null object to be returned!");
        obj.gameObject.SetActive(false);
        pool.Add(obj);
    }
}
