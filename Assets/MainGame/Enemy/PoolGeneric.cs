using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolGeneric<T>
{
    [SerializeField] public GameObject _originalPrefab;
    [SerializeField] private List<GameObject> _usedPool = new List<GameObject>();
    [SerializeField] private List<GameObject> _availablePool = new List<GameObject>();

    public GameObject GetFromPool()
    {
        GameObject pooledObject;

        if (_availablePool.Count == 0)
        {
            // Instantiate
            pooledObject = GameObject.Instantiate(_originalPrefab);
        }

        else
        {
            // Get First From Pool List
            pooledObject = _availablePool[0];
        }

        _usedPool.Add(pooledObject);
        _availablePool.Remove(pooledObject);

        return pooledObject;
    }

    public void Recycle(GameObject recycle)
    {
        recycle.SetActive(false);
        recycle.transform.position = new Vector3(100, 0, 100);

        _usedPool.Remove(recycle);
        _availablePool.Add(recycle);
    }

    public void Clear()
    {
        //_usedPool.Clear();
        _availablePool.Clear();
    }

    public void InitializePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var instance = GetFromPool();
        }

        for (int i = _usedPool.Count - 1; i >= 0; i--)
        {
            Recycle(_usedPool[i]);
        }
    }
}
