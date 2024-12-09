using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Prefab de Proyectiles y Obst�culos")]
    public GameObject projectilePrefab;
    public GameObject obstaclePrefab;
    public GameObject conePrefab;

    [Header("Ataques")]
    public List<IAttack> attacks = new List<IAttack>();

    // M�todo Start que inicializa la salud y los ataques
    private void Start()
    {
        // Agregar Rigidbody2D si no tiene uno
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.isKinematic = true;  // Evita que se vea afectado por la f�sica

        // Asegurarse de que el enemigo tiene un collider
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider2D>(); // Puedes usar BoxCollider2D o el que corresponda
            col.isTrigger = true;  // Activamos el trigger
        }

        currentHealth = maxHealth;

        // Crear f�bricas
        IObjectFactory projectileFactory = new ProjectileFactory(projectilePrefab);
        IObjectFactory obstacleFactory = new ObstacleFactory(obstaclePrefab);
        IObjectFactory coneFactory = new ObstacleFactory(conePrefab); // Reutilizamos ObstacleFactory

        // Agregar ataques con las f�bricas
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

    // M�todo que ejecuta todos los ataques
    private void ExecuteAllAttacks()
    {
        foreach (var attack in attacks)
        {
            attack.ExecuteAttack(this);
        }
    }

    // M�todo que reduce la vida al recibir da�o
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Eliminar enemigo si su salud es 0 o menos
            Debug.Log("ff");
        }
    }

    // Detecci�n de colisi�n con un proyectil
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si la colisi�n fue con un proyectil
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("Colisi�n detectada con Projectile");
            TakeDamage(1);  // Reducir vida en 1
            other.gameObject.SetActive(false);
        }
    }
}
