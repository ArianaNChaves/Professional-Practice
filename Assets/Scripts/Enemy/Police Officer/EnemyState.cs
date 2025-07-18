using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public enum STATE { IDLE,CHASE, ATTACK, DEATH}
    protected STATE state;
    protected EnemyState NextEnemyState;
    protected enum EVENT {ENTER, UPDATE, EXIT}
    protected EVENT stage;

    public EnemyState()
    {
        state = STATE.IDLE;
        stage = EVENT.ENTER;
        NextEnemyState = this;
    }
    public virtual void Enter() {stage = EVENT.UPDATE;}

    public virtual void Update() {stage = EVENT.UPDATE;}
    public virtual void Exit() {stage = EVENT.EXIT;}

    public EnemyState Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return NextEnemyState;
        }
        return this;
    }

    public void ChangeState(STATE to)
    {
        state = to;
        stage = EVENT.ENTER;
    }
    
    public STATE GetState() => state;
    
}
