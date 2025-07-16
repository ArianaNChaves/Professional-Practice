using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Initial : EnemyState
{
    public Initial(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.INITIAL;
        npc = _npc;
    }
    public override void Enter()
    {
        //efecto/animacion al spawnear enemigo
        //anim.SetTrigger("isSpawning);
        
        base.Enter();
    }

    public override void Update()
    {
        NextEnemyState = new Chase(npc, agent, anim, player);
        stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        //efecto / animacion al terminar de spawnear y empezar a chasear al player
        //anim.ResetTrigger("isSpawning");
        base.Exit();
    }
    
}
