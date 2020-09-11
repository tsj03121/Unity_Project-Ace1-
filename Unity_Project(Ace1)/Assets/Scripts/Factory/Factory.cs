using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    List<T> pool_ = new List<T>();
    int defaultPoolSize_;
    T prefab_;

    public int DefaultPoolSize { get; }

    public Factory(T prefab, int defaultPoolSize = 5)
    {
        prefab_ = prefab;
        defaultPoolSize_ = defaultPoolSize;

        Debug.Assert(prefab_ != null, "Prefab is null!");
    }

    void CreatePool()
    {
        for(int i = 0; i < defaultPoolSize_; ++i)
        {
            T obj = GameObject.Instantiate(prefab_) as T;
            obj.gameObject.SetActive(false);
            pool_.Add(obj);
        }
    }

    public T Get()
    {
        if(pool_.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = pool_.Count - 1;
        T obj = pool_[lastIndex];
        pool_.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
            
        return obj;
    }

    public void Restore(T obj)
    {
        Debug.Assert(obj != null, "Null object to be returned!");
        obj.gameObject.SetActive(false);
        pool_.Add(obj);
    }
}
