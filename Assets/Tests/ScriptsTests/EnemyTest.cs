using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTest : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Prefab de Proyectiles y Obstáculos")]
    public GameObject projectilePrefab;
    public GameObject obstaclePrefab;
    public GameObject conePrefab; // Prefab del cono

    [Header("Ataques")]
    public List<IAttack> attacks = new List<IAttack>();

    private void Start()
    {
        currentHealth = maxHealth;
        attacks.Add(new ShootAtPlayerAttack
        {
            shootingInterval = 2f,
            projectileSpeed = 5f,
            projectilePrefab = projectilePrefab
        });

        attacks.Add(new RandomObstacleAttack
        {
            obstaclePrefab = obstaclePrefab,
            obstacleSpawnInterval = 5f
        });

        attacks.Add(new ConeAttack
        {
            conePrefab = conePrefab,
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        GameManager.SetEnemyDefeated(true);
        GameManager.Instance.EndGame();
        Invoke("LoadMenu", 5f);
        Destroy(gameObject);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}


