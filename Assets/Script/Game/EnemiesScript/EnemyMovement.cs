using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class EnemyMovement : NetworkBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private float checkInterval = 0.5f;
    
    [SerializeField]
    private float attackRange = 2.0f;
    
    [SerializeField]
    private float attackRate = 1.0f;

    [SerializeField]
    private float attackDamage = 10f;

    private float nextAttackTime = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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

                if (Vector3.Distance(transform.position, closestPlayer.position) <= attackRange && Time.time >= nextAttackTime)
                {
                    Attack(closestPlayer);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
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

    void Attack(Transform target)
    {
        Debug.Log("Attacking " + target.name);

        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.CmdTakeDamage(attackDamage);
        }
    }
}
