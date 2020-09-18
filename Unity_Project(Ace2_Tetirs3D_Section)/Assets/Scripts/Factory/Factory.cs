using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    List<GameObject> pool_ = new List<GameObject>();
    int defaultPoolSize_;
    GameObject prefab_;

    public int DefaultPoolSize { get; }

    public Factory(GameObject prefab, int defaultPoolSize = 20)
    {
        prefab_ = prefab;
        defaultPoolSize_ = defaultPoolSize;
    }

    void CreatePool()
    {
        for(int i = 0; i < defaultPoolSize_; ++i)
        {
            GameObject obj = GameObject.Instantiate(prefab_) as GameObject;
            obj.gameObject.SetActive(false);
            pool_.Add(obj);
        }
    }

    public GameObject Get()
    {
        if(pool_.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = pool_.Count - 1;
        GameObject obj = pool_[lastIndex];
        pool_.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
            
        return obj;
    }

    public void Restore(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        pool_.Add(obj);
    }
}
