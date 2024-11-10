using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas
using UnityEngine.UI; // Para manejar la UI

public class GameManager : MonoBehaviour
{
    public Text resultText; // Asigna el componente Text en el Inspector
    public Text timerText; // Asigna el componente Text para mostrar el tiempo
    private static GameManager _instance; // Instancia estática

    private static bool playerDefeated = false; // Para determinar si el jugador fue derrotado
    private static bool enemyDefeated = false; // Para determinar si el enemigo fue derrotado
    private float elapsedTime = 0f; // Tiempo transcurrido
    private bool isGameActive = false; // Para controlar el estado del juego

    public static GameManager Instance // Propiedad para acceder a la instancia
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is null!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Mantener la instancia entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruir el objeto si ya existe una instancia
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            elapsedTime += Time.deltaTime; // Aumenta el tiempo transcurrido
            UpdateTimerText(); // Actualiza el texto del temporizador
        }
    }

    private void Start()
    {
        isGameActive = true; // Activa el juego
        elapsedTime = 0f; // Reinicia el temporizador
        SceneManager.LoadScene("Level1"); // Cambia a la escena "SampleScene"
    }
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Si estás en el editor, detener la reproducción
#endif
    }

    public void EndGame()
    {
        isGameActive = false; // Detiene el contador al final del juego
        ShowResult(); // Muestra el resultado al final
        SceneManager.LoadScene("Menu"); // Cambia a la escena "Menu"
    }

    public static void SetPlayerDefeated(bool defeated)
    {
        playerDefeated = defeated;
    }

    public static void SetEnemyDefeated(bool defeated)
    {
        enemyDefeated = defeated;
    }

    private void UpdateTimerText()
    {
        timerText.text = "Tiempo: " + elapsedTime.ToString("F1");
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Formato MM:SS
    }

    private void ShowResult()
    {
        if (playerDefeated)
        {
            resultText.text = "Derrota";
        }
        else if (enemyDefeated)
        {
            resultText.text = "Victoria\nTiempo: " + elapsedTime.ToString("F1");
        }
        else
        {
            resultText.text = ""; // Si no hay resultados, mostrar vacío
        }
    }
}
