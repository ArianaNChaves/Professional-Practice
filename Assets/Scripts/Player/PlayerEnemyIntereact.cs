using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyIntereact : MonoBehaviour
{
    private void OnEnable()
    {
        Enemy.OnSpawn += GivePlayerGameObject;
    }
    private void OnDisable()
    {
        Enemy.OnSpawn -= GivePlayerGameObject;
    }

    private void GivePlayerGameObject(Enemy newEnemy)
    {
        newEnemy.SetPlayer(gameObject);
    }
}
