using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyMachine
{
    public EnemyAttack(MonoBehaviour enemyInstance) : base()
    {
        CurrentState = STATE.ATTACK;
    }
    
    public override void Enter()
    {
        base.Enter();
        //efecto/animacion al spawnear enemigo
        //anim.SetTrigger("isSpawning);
        
    }

    public override void Update()
    {
        // NextEnemyState = new Chase(npc, agent, anim, player);
        // stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        //efecto / animacion al terminar de spawnear y empezar a chasear al player
        //anim.ResetTrigger("isSpawning");
        base.Exit();
    }
}
