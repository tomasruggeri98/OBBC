using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3; // Vida máxima del enemigo
    private int currentHealth;

    public GameObject projectilePrefab; // Prefab del proyectil del enemigo
    public float shootingInterval = 2f; // Intervalo de disparo en segundos
    public float projectileSpeed = 5f; // Velocidad del proyectil

    private Transform player; // Referencia al jugador

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Iniciar el disparo automático
        InvokeRepeating("ShootAtPlayer", shootingInterval, shootingInterval);
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para disparar al jugador
    void ShootAtPlayer()
    {
        if (player == null) return; // Salir si el jugador no está en escena

        // Calcular la dirección hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;

        // Crear el proyectil
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Aplicar velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        Destroy(projectile, 4f);
    }

    // Método para destruir el enemigo cuando su vida llega a 0
    void Die()
    {
        GameManager.SetEnemyDefeated(true);
        GameManager.Instance.EndGame(); // Detener el temporizador y mostrar el resultado
        Invoke("LoadMenu", 5f); // Esperar 5 segundos y cargar el menú
        Destroy(gameObject); // Destruir el enemigo
    }

    // Método de colisión con proyectiles etiquetados como "Projectile"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile")) // Tag del proyectil del jugador
        {
            TakeDamage(1); // Reducir la vida en 1
            Destroy(collision.gameObject); // Destruir el proyectil del jugador
        }
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu"); // Cambia a la escena "Menu"
    }
}
