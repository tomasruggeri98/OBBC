using UnityEngine;

[System.Serializable]
public class RandomObstacleAttack : IAttack
{
    [Header("Obstáculo Aleatorio")]
    public float obstacleSpawnInterval = 5f; // Intervalo en segundos para generar obstáculos

    private IObjectFactory obstacleFactory;

    public RandomObstacleAttack(IObjectFactory factory)
    {
        this.obstacleFactory = factory;
    }

    public void ExecuteAttack(EnemyTest enemy)
    {
        float angle = Random.Range(0f, 360f);
        Vector2 spawnPosition = new Vector2(
            enemy.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * 5f, // Radio del círculo donde se genera
            enemy.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * 5f
        );

        GameObject obstacle = obstacleFactory.CreateObject(spawnPosition, Quaternion.identity);
        Object.Destroy(obstacle, 5f); // Tiempo de vida del obstáculo
    }
}
