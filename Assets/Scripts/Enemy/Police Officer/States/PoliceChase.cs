using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceChase : EnemyState
{
    public PoliceChase()
    {
        stage = EVENT.ENTER;
        state = STATE.CHASE;
    }
}

