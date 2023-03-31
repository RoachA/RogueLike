using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> objectPool = new Queue<T>();
    private readonly HashSet<T> activeObjects = new HashSet<T>();
    private readonly Transform parentTransform;

    public ObjectPool(T prefab, int initialPoolSize = 0)
    {
        this.prefab = prefab;
        parentTransform = new GameObject("ObjectPool - " + prefab.name).transform;
        parentTransform.gameObject.SetActive(false);
        for (int i = 0; i < initialPoolSize; i++) objectPool.Enqueue(InstantiateObject());
    }

    private T InstantiateObject()
    {
        T obj = Object.Instantiate(prefab, parentTransform, true);
        return obj;
    }

    public T GetObject(Transform parent = null)
    {
        T obj = objectPool.Count > 0 ? objectPool.Dequeue() : InstantiateObject();
        activeObjects.Add(obj);
        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReleaseObject(T obj)
    {
        obj.transform.SetParent(parentTransform);
        activeObjects.Remove(obj);
        objectPool.Enqueue(obj);
    }
}