using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : EnemyState
{
    private Vector3 _playerDirection;
    public Chase(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.CHASE;
        agent.speed = 6.0f;
        agent.isStopped = false;
        agent.stoppingDistance = 10;
    }

    public override void Enter()
    {
        //anim.SetTrigger(isChasing);
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        npc.transform.LookAt(player);
        
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                NextEnemyState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isRunning");
        base.Exit();
    }

    private bool CanAttackPlayer()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            canAttack = false;
            return false;
        }
        else
        {
            canAttack = true;
            return true;
        }
    }
}
