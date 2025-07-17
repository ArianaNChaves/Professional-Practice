using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    [SerializeField] private EnemySO enemyData;
    
    public enum STATE { IDLE,CHASE, ATTACK, DEATH }
    protected STATE state;
    protected enum EVENT {ENTER, UPDATE, EXIT}
    protected EVENT stage;
    protected EnemyState NextEnemyState;
    
    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public EnemyState Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter();
        }
        if (stage == EVENT.UPDATE)
        {
            Update();
        }
        if (stage == EVENT.ENTER)
        {
            Exit();
            return NextEnemyState;
        }
        
        Debug.Log($"Stage {stage}");
        return this;
    }

    public void ChangeState(STATE to)
    {
        state = to;
    }
    
    public STATE GetState()
    {
        return state;
    }
    
}
