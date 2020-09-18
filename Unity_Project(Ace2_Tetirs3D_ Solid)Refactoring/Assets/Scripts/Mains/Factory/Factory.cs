using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    List<GameObject> _pool = new List<GameObject>();
    int _defaultPoolSize;
    GameObject _prefab;

    public Factory(GameObject prefab, int defaultPoolSize = 20)
    {
        _prefab = prefab;
        _defaultPoolSize = defaultPoolSize;
    }

    void CreatePool()
    {
        for(int i = 0; i < _defaultPoolSize; ++i)
        {
            GameObject obj = GameObject.Instantiate(_prefab) as GameObject;
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        if(_pool.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = _pool.Count - 1;
        GameObject obj = _pool[lastIndex];
        _pool.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
            
        return obj;
    }

    public void Restore(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }
}
