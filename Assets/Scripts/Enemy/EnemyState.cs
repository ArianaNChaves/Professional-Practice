using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    public enum STATE
    {
        INITIAL, CHASE, ATTACK, WAIT, KILL, DIE
    }
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    public float attackRange;
    protected EVENT stage;
    protected bool canAttack;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected Transform secondPlayer;
    protected EnemyState NextEnemyState;
    protected NavMeshAgent agent;
    
    public EnemyState(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER;
    }
    
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
        if (stage == EVENT.EXIT)
        {
            Exit();
            return NextEnemyState;
        }
        return this;
    }
}
