using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wait : EnemyState
{
    public Wait(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.WAIT;
        agent.isStopped = true;
        
    }
    public override void Enter()
    {
        //anim.SetTrigger("isWaiting");
        base.Enter();
    }

    public override void Update()
    {
       // stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isWaiting");
        NextEnemyState = new Initial(npc, agent, anim, player);
        base.Exit();
    }
}
