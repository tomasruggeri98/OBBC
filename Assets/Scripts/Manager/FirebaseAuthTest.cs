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
            SignInAnonymously(); // Intentar autenticaci�n
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
                // Aqu� obtenemos el usuario correctamente
                FirebaseUser newUser = task.Result.User;
                Debug.Log($"Autenticaci�n an�nima exitosa. UID: {newUser.UserId}");
            }
            else
            {
                Debug.LogError($"Error en la autenticaci�n: {task.Exception}");
            }
        });
    }
}
