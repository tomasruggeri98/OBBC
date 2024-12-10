using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas
using UnityEngine.UI; // Para manejar la UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    [SerializeField] int currentHealth;
    //public GameManager gameManager;
    public Text healthText; 

    // Daño configurable para cada tipo de ataque
    public int projectileDamage;
    public int obstacleDamage;
    public int zoneDamage;

   public void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si el jugador entra en un trigger de ObstaculeAttack o ZoneAttack
        if (other.CompareTag("ProjectileEnemy"))
        {
            TakeDamage(projectileDamage);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("ObstaculeAttack"))
        {
            TakeDamage(obstacleDamage); 
        }
        else if (other.CompareTag("ZoneAttack"))
        {
            TakeDamage(zoneDamage); 
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText(); 

        if (currentHealth <= 0)
        {
            GameOver(); 
        }
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

   public void UpdateHealthText()
    {
        healthText.text = "Vida: " + currentHealth; // Actualiza el texto en pantalla
    }

    public void GameOver()
    {
        GameManager.SetPlayerDefeated(true);
        GameManager.Instance.EndGame(); 
        Invoke("LoadMenu", 5f); 
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
