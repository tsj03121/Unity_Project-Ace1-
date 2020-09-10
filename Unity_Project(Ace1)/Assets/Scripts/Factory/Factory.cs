using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    List<RecycleObject> pool_ = new List<RecycleObject>();
    int defaultPoolSize_;
    RecycleObject prefab_;

    int sortingOrder = 0;

    public Factory(RecycleObject prefab, int defaultPoolSize = 5)
    {
        prefab_ = prefab;
        defaultPoolSize_ = defaultPoolSize;

        Debug.Assert(prefab_ != null, "Prefab is null!");
    }

    void CreatePool()
    {
        for(int i = 0; i < defaultPoolSize_; ++i)
        {
            RecycleObject obj = GameObject.Instantiate(prefab_) as RecycleObject;
            obj.gameObject.SetActive(false);
            pool_.Add(obj);
        }
    }

    public RecycleObject Get()
    {
        if(pool_.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = pool_.Count - 1;
        RecycleObject obj = pool_[lastIndex];
        SpriteRenderer spriteRenderer = obj.GetSpriteRenderer();
        sortingOrder += 1;
        spriteRenderer.sortingOrder = sortingOrder;
        pool_.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
            
        return obj;
    }

    public void Restore(RecycleObject obj)
    {
        Debug.Assert(obj != null, "Null object to be returned!");
        obj.gameObject.SetActive(false);
        pool_.Add(obj);
    }
}
