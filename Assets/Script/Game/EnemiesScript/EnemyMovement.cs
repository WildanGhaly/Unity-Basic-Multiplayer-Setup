using UnityEngine;
using Mirror;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : NetworkBehaviour
{
    private NavMeshAgent navMeshAgent;
    private EnemyAttack enemyAttack;

    [SerializeField]
    private float checkInterval = 0.5f;

    [SerializeField]
    private float attackRange = 2.0f;

    private bool isAttacking;
    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        if (isServer)
        {
            StartCoroutine(UpdateTarget());
        }
    }

    IEnumerator UpdateTarget()
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
                }
                else
                {
                    isAttacking = false;
                }
            }
            enemyAnimator.SetBool("IsAttacking", isAttacking);
            yield return new WaitForSeconds(checkInterval);
        }
    }

    Transform FindClosestPlayer()
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
}
