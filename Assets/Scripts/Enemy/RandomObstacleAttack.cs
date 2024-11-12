using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomObstacleAttack : IAttack
{
    [Header("Obstáculo Aleatorio")]
    public GameObject obstaclePrefab;
    public float obstacleSpawnInterval = 5f; // Intervalo en segundos para generar obstáculos

    public void ExecuteAttack(EnemyTest enemy)
    {
        float angle = Random.Range(0f, 360f);
        Vector2 spawnPosition = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * 5f, // Radio del círculo donde se genera
            Mathf.Sin(angle * Mathf.Deg2Rad) * 5f
        );

        GameObject obstacle = Object.Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        Object.Destroy(obstacle, 5f); // Tiempo de vida del obstáculo
    }
}

