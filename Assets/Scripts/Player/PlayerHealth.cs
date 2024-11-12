using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas
using UnityEngine.UI; // Para manejar la UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Máxima cantidad de vidas
    private int currentHealth;

    public Text healthText; // Referencia al componente Text para mostrar la salud en pantalla

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
            TakeDamage(); // Llama a la función para descontar salud
            Destroy(collision.gameObject); // Destruye el proyectil enemigo
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        UpdateHealthText(); // Actualiza el texto de salud

        if (currentHealth <= 0)
        {
            GameOver(); // Llama a la función GameOver si las vidas llegan a 0
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Vida: " + currentHealth; // Actualiza el texto en pantalla
    }

    void GameOver()
    {
        GameManager.SetPlayerDefeated(true);
        GameManager.Instance.EndGame(); // Detener el temporizador y mostrar el resultado
        Invoke("LoadMenu", 5f); // Esperar 5 segundos y cargar el menú
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu"); // Cambia a la escena "Menu"
    }
}
