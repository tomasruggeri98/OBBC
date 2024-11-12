using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3; // Vida m�xima del enemigo
    private int currentHealth;

    public GameObject projectilePrefab; // Prefab del proyectil del enemigo
    public float shootingInterval = 2f; // Intervalo de disparo en segundos
    public float projectileSpeed = 5f; // Velocidad del proyectil

    private Transform player; // Referencia al jugador

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Iniciar el disparo autom�tico
        InvokeRepeating("ShootAtPlayer", shootingInterval, shootingInterval);
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para disparar al jugador
    void ShootAtPlayer()
    {
        if (player == null) return; // Salir si el jugador no est� en escena

        // Calcular la direcci�n hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;

        // Crear el proyectil
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Aplicar velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        Destroy(projectile, 4f);
    }

    // M�todo para destruir el enemigo cuando su vida llega a 0
    void Die()
    {
        GameManager.SetEnemyDefeated(true);
        GameManager.Instance.EndGame(); // Detener el temporizador y mostrar el resultado
        Invoke("LoadMenu", 5f); // Esperar 5 segundos y cargar el men�
        Destroy(gameObject); // Destruir el enemigo
    }

    // M�todo de colisi�n con proyectiles etiquetados como "Projectile"
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
