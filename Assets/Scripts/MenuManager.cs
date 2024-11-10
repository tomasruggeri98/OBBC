using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena
using UnityEngine.UI; // Para manejar la UI

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1"); // Cambia a la escena "SampleScene"
    }
    public void LevelManagerScene()
    {
        SceneManager.LoadScene("LevelManager"); 
    }
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Si estás en el editor, detener la reproducción
#endif
    }
}
