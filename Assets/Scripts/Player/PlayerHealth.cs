using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas
using UnityEngine.UI; // Para manejar la UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // M�xima cantidad de vidas
    [SerializeField] int currentHealth;
    //public GameManager gameManager;
    public Text healthText; // Referencia al componente Text para mostrar la salud en pantalla

    // Da�o configurable para cada tipo de ataque
    public int projectileDamage;
    public int obstacleDamage;
    public int zoneDamage;

    void Start()
    {
        currentHealth = maxHealth; // Inicializar vidas
        UpdateHealthText(); // Actualiza el texto al inicio
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar si el jugador colisiona con un proyectil enemigo
        if (collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            TakeDamage(projectileDamage); // Descontar salud seg�n el da�o del proyectil
            Destroy(collision.gameObject); // Destruye el proyectil enemigo
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si el jugador entra en un trigger de ObstaculeAttack o ZoneAttack
        if (other.CompareTag("ObstaculeAttack"))
        {
            TakeDamage(obstacleDamage); // Descontar salud seg�n el da�o del obst�culo
        }
        else if (other.CompareTag("ZoneAttack"))
        {
            TakeDamage(zoneDamage); // Descontar salud seg�n el da�o de la zona
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText(); // Actualiza el texto de salud

        if (currentHealth <= 0)
        {
            GameOver(); // Llama a la funci�n GameOver si las vidas llegan a 0
        }
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void UpdateHealthText()
    {
        healthText.text = "Vida: " + currentHealth; // Actualiza el texto en pantalla
    }

    void GameOver()
    {
        GameManager.SetPlayerDefeated(true);
        GameManager.Instance.EndGame(); // Detener el temporizador y mostrar el resultado
        Invoke("LoadMenu", 5f); // Esperar 5 segundos y cargar el men�
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu"); // Cambia a la escena "Menu"
    }
}
