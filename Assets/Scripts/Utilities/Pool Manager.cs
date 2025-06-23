using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviourSingleton<PoolManager>
{
    private Dictionary<Type, IPooleable> prefabs = new Dictionary<Type, IPooleable>();
    private Dictionary<Type, Queue<IPooleable>> pool = new Dictionary<Type, Queue<IPooleable>>();


    protected override void OnAwaken() {}
    
    public T Get<T>() where T : MonoBehaviour, IPooleable
    {
        bool hasKey = false;
        foreach (var myObject in pool)
        {
            if (myObject.Key == typeof(T))
            {
                if (myObject.Value.Count > 0)
                {
                    return (T)myObject.Value.Dequeue();
                }
                else
                {
                    hasKey = true;
                    break;
                }
            }
        }

        foreach (var prefab in prefabs)
        {
            if (prefab.Key == typeof(T))
            {
                if (!hasKey)
                {
                    pool.Add(typeof(T), new Queue<IPooleable>());
                }
                return GameObject.Instantiate((MonoBehaviour)prefab.Value, transform) as T;
            }
        }
        
        return null;
    }
    
    public void ReturnToPool<T>(T pooledObject) where T : MonoBehaviour, IPooleable
    {
        if (!pool.ContainsKey(typeof(T)))
        {
            pooledObject.gameObject.SetActive(false);
            pool.Add(typeof(T), new Queue<IPooleable>());
            pool[typeof(T)].Enqueue(pooledObject);  
        }
        else
        {
            pooledObject.gameObject.SetActive(false);
            pool[typeof(T)].Enqueue(pooledObject);
        }
        
    }

    public void InitializePool<T>(T prefab, int minSize = 10) where T : MonoBehaviour, IPooleable
    {
        var type = typeof(T);
        if (!prefabs.ContainsKey(type))
        {
            prefabs[type] = prefab;
        }

        if (!pool.ContainsKey(type))
        {
            pool[type] = new Queue<IPooleable>();
        }

        for (int i = 0; i < minSize; i++)
        {
            T instance = Instantiate(prefab, transform);
            instance.gameObject.SetActive(false);
            pool[type].Enqueue(instance);
        }
    }
}
