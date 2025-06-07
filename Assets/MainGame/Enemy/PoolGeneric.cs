using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PoolGeneric<T>: IPooleable
{
    [SerializeField] public GameObject originalPrefab;
    [SerializeField] private List<GameObject> usedPool = new();
    [SerializeField] private List<GameObject> availablePool = new();

    public GameObject GetFromPool()
    {
        GameObject pooledObject;

        if (availablePool.Count == 0)
        {
            if (originalPrefab == null)
            {
           
                return null;
            }

            pooledObject = GameObject.Instantiate(originalPrefab);
        }
        else
        {
            pooledObject = availablePool[0];
            availablePool.RemoveAt(0);
        }

        pooledObject.SetActive(true);
        usedPool.Add(pooledObject);

        return pooledObject;
    }
    public void Clear()
    {
        foreach (var obj in availablePool)
        {
            GameObject.Destroy(obj);
        }
        availablePool.Clear();
    }

    public void InitializePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var instance = GameObject.Instantiate(originalPrefab);
            instance.SetActive(false);
            instance.transform.position = new Vector3(100, 0, 100);
            availablePool.Add(instance);

        }
    }

    public void Recycle(GameObject obj)
    {
        if (availablePool.Contains(obj)) return;
        usedPool.Remove(obj);
        availablePool.Add(obj);
        obj.SetActive(false);
        obj.transform.position = new Vector3(100, 0, 100);
    }
}