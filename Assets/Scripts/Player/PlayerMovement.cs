using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento Circular")]
    public float radius = 3f; // Radio del c�rculo
    public float speed = 50f; // Velocidad de movimiento
    private float angle = 0f; // �ngulo inicial
    public bool clockwise = true; // Direcci�n de movimiento

    [Header("Configuraci�n de Restricciones")]
    public bool invomilizacion = false; // Bloquear cambio de direcci�n

    private PlayerInput controls; // Ahora usamos PlayerInput como clase


    private void Awake()
    {
        // Inicializar el sistema de entrada
        controls = new PlayerInput();

        // Vincular las acciones
        controls.Player.ChangeDirection.performed += OnChangeDirection;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void Update()
    {
        MovementPlayer();
    }

    void MovementPlayer()
    {
        // Actualizar el �ngulo basado en la direcci�n
        angle += (clockwise ? 1 : -1) * speed * Time.deltaTime;

        // Calcular la nueva posici�n en la �rbita circular
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        // Actualizar la posici�n del jugador
        transform.position = new Vector3(x, y, 0);
    }

    public void OnChangeDirection(InputAction.CallbackContext context)
    {
        // Cambiar direcci�n si no est� inmovilizado
        if (!invomilizacion)
        {
            clockwise = !clockwise;
        }
    }
}
