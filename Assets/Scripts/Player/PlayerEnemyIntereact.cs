using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyIntereact : MonoBehaviour
{
    private void OnEnable()
    {
        DeprecatedEnemy.OnSpawn += GivePlayerGameObject;
    }
    private void OnDisable()
    {
        DeprecatedEnemy.OnSpawn -= GivePlayerGameObject;
    }

    private void GivePlayerGameObject(DeprecatedEnemy newDeprecatedEnemy)
    {
        newDeprecatedEnemy.SetPlayer(gameObject);
    }
}
