using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceDeath : EnemyState
{
    public PoliceDeath()
    {
        stage = EVENT.ENTER;
        state = STATE.DEATH;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
