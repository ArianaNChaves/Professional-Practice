using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAttack : PoliceOfficerState
{
    public PoliceAttack()
    {
        stage = EVENT.ENTER;
        state = STATE.ATTACK;
    }
}
