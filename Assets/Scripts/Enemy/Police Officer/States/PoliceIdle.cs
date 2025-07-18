using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceIdle : EnemyState
{
    public PoliceIdle()
    {
        stage = EVENT.ENTER;
        state = STATE.IDLE;
    }
    
}
