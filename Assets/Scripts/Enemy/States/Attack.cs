using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : EnemyState
{
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
        agent.isStopped = true;
    }

    public override void Enter()
    {
        //anim.SetTrigger("isAttacking");
        base.Enter();
    }

    public override void Update()
    {
        npc.transform.LookAt(player);
        if (!canAttack)
        {
            NextEnemyState = new Chase(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isAttacking");
        base.Exit();
    }
    
    
}
