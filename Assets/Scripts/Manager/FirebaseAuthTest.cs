using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class FirebaseAuthTest : MonoBehaviour
{
    private FirebaseAuth auth;

    async void Start()
    {
        // Inicializar Firebase
        await FirebaseApp.CheckAndFixDependenciesAsync();

        if (FirebaseApp.DefaultInstance != null)
        {
            auth = FirebaseAuth.DefaultInstance;
            Debug.Log("Firebase inicializado correctamente.");
            SignInAnonymously(); // Intentar autenticación
        }
        else
        {
            Debug.LogError("No se pudo inicializar Firebase.");
        }
    }

    void SignInAnonymously()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                // Aquí obtenemos el usuario correctamente
                FirebaseUser newUser = task.Result.User;
                Debug.Log($"Autenticación anónima exitosa. UID: {newUser.UserId}");
            }
            else
            {
                Debug.LogError($"Error en la autenticación: {task.Exception}");
            }
        });
    }
}
