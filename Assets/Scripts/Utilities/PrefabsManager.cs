using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    [SerializeField] private Enemy normalEnemyPrefab;
    [SerializeField] private int normalEnemyPrefabSize;
    private bool _isPoolInitialized = false;
    void Start()
    {
        if (_isPoolInitialized) return;
        _isPoolInitialized = true;
        PoolManager.Instance.InitializePool(normalEnemyPrefab, normalEnemyPrefabSize);
    }


}
