using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachine : EventMachine
{
    public enum STATE {IDLE, CHASE, ATTACK, DIE};
    public STATE CurrenEnemytState;

    public EnemyMachine()
    {
        CurrenEnemytState = (STATE)EventStage;
    }
}
