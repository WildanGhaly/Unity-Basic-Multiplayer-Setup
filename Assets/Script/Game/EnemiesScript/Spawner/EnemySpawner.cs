using UnityEngine;
using Mirror;
using System.Collections;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 10f;

    public override void OnStartServer()
    {
        StartCoroutine(SpawnEnemiesPeriodically());
    }

    [Server]
    void SpawnEnemyAtRandomPoint()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        NetworkServer.Spawn(enemy);
    }

    IEnumerator SpawnEnemiesPeriodically()
    {
        while (true)
        {
            SpawnEnemyAtRandomPoint();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}