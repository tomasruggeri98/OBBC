using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomObstacleAttack : IAttack
{
    [Header("Obst�culo Aleatorio")]
    public GameObject obstaclePrefab;
    public float obstacleSpawnInterval = 5f; // Intervalo en segundos para generar obst�culos

    public void ExecuteAttack(EnemyTest enemy)
    {
        float angle = Random.Range(0f, 360f);
        Vector2 spawnPosition = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * 5f, // Radio del c�rculo donde se genera
            Mathf.Sin(angle * Mathf.Deg2Rad) * 5f
        );

        GameObject obstacle = Object.Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        Object.Destroy(obstacle, 5f); // Tiempo de vida del obst�culo
    }
}

