using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachine : MonoBehaviour
{
    public enum STATE {IDLE, CHASE, ATTACK, DIE};
    public STATE CurrenEnemytState;

    protected string enemyName;
}
