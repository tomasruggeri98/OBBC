using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Prefab de Proyectiles y Obstáculos")]
    public GameObject projectilePrefab;
    public GameObject obstaclePrefab;
    public GameObject conePrefab;

    [Header("Ataques")]
    public List<IAttack> attacks = new List<IAttack>();

    private void Start()
    {
        currentHealth = maxHealth;

        // Crear fábricas
        IObjectFactory projectileFactory = new ProjectileFactory(projectilePrefab);
        IObjectFactory obstacleFactory = new ObstacleFactory(obstaclePrefab);
        IObjectFactory coneFactory = new ObstacleFactory(conePrefab); // Reutilizamos ObstacleFactory

        // Agregar ataques con las fábricas
        attacks.Add(new ShootAtPlayerAttack(projectileFactory)
        {
            shootingInterval = 2f,
            projectileSpeed = 5f
        });

        attacks.Add(new RandomObstacleAttack(obstacleFactory)
        {
            obstacleSpawnInterval = 5f
        });

        attacks.Add(new ConeAttack(coneFactory)
        {
            radius = 3f,
            angleRange = 45f,
            attackInterval = 5f
        });

        InvokeRepeating(nameof(ExecuteAllAttacks), 2f, 5f);
    }

    private void ExecuteAllAttacks()
    {
        foreach (var attack in attacks)
        {
            attack.ExecuteAttack(this);
        }
    }
}
