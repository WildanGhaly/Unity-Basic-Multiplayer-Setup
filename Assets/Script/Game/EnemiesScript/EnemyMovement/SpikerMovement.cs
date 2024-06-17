using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikerMovement : EnemyMovement
{
    protected override IEnumerator UpdateTarget()
    {
        while (true)
        {
            Transform closestPlayer = FindClosestPlayer();
            if (closestPlayer != null)
            {
                navMeshAgent.SetDestination(closestPlayer.position);

                if (Vector3.Distance(transform.position, closestPlayer.position) <= attackRange)
                {
                    enemyAnimator.SetTrigger("TriggerAttack");
                    enemyAttack.Attack(closestPlayer);
                    RpcAttack();
                }
                else
                {
                    RpcRun();
                }
            }
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
