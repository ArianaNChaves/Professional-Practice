using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    [SerializeField] private Enemy normalEnemyPrefab;
    [SerializeField] private int normalEnemyPrefabSize;
    void Start()
    {
        PoolManager.Instance.InitializePool(normalEnemyPrefab, normalEnemyPrefabSize);
    }


}
