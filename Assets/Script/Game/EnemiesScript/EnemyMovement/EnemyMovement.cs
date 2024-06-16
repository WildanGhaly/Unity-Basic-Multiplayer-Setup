using UnityEngine;
using Mirror;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : NetworkBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected EnemyAttack enemyAttack;

    [SerializeField]
    protected float checkInterval = 0.5f;

    [SerializeField]
    protected float attackRange = 2.0f;

    protected bool isAttacking;
    protected Animator enemyAnimator;

    public override void OnStartServer()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAnimator = GetComponent<Animator>();

        StartCoroutine(UpdateTarget());
    }

    protected virtual IEnumerator UpdateTarget()
    {
        while (true)
        {
            Transform closestPlayer = FindClosestPlayer();
            if (closestPlayer != null)
            {
                navMeshAgent.SetDestination(closestPlayer.position);

                if (Vector3.Distance(transform.position, closestPlayer.position) <= attackRange)
                {
                    if (!isAttacking) enemyAnimator.SetTrigger("TriggerAttack");
                    isAttacking = true;
                    enemyAttack.Attack(closestPlayer);
                    enemyAnimator.SetBool("IsWalking", false);
                    enemyAnimator.SetBool("IsRunning", false);
                }
                else
                {
                    enemyAnimator.SetBool("IsWalking", true);
                    enemyAnimator.SetBool("IsRunning", true);
                    isAttacking = false;
                }
            }
            enemyAnimator.SetBool("IsAttacking", isAttacking);
            yield return new WaitForSeconds(checkInterval);
        }
    }

    protected virtual Transform FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }

        return closestPlayer;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
