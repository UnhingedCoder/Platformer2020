﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : StateMachineBehaviour
{
    EnemyBossController enemyBoss;
    EnemyPatrolController enemyPatrol;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyBoss = animator.GetComponent<EnemyBossController>();
        enemyPatrol = animator.GetComponent<EnemyPatrolController>();
        enemyPatrol.CanMove = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyPatrol.ChangeFacingDirection(enemyBoss.DetectPlayerDirection());
        if (Vector2.Distance(enemyBoss.playerTarget.transform.position, enemyBoss.transform.position) <= enemyBoss.attackRange)
        {
            enemyBoss.EnemyProjectile.FireProjectiles(enemyBoss.projectilesToShoot);
        }
        else
        {
            animator.SetTrigger("Chase");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        enemyBoss.EnemyProjectile.ResetFireTime();
        animator.ResetTrigger("Chase");
    }
}