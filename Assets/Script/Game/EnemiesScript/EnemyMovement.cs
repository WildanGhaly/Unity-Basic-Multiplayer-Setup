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
}
