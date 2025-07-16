using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachine : EventMachine
{
    public enum STATE {IDLE, CHASE, ATTACK, DIE};

    public EnemyMachine()
    {
        Stage = EVENT.ENTER;
    }
}
