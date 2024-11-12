using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 3f; // Radio del círculo
    public float speed = 50f; // Velocidad de movimiento
    private float angle = 0f; // Ángulo inicial
    private bool clockwise = true; // Dirección de movimiento
    public bool invomilizacion;

    void Update()
    {
        MovementPlayer();
    }
    void MovementPlayer()
    {
        // Cambiar dirección al presionar la tecla "A"
        if (Input.GetKeyDown(KeyCode.A) && !invomilizacion)
        {
            clockwise = !clockwise; // Cambiar la dirección
        }

        // Actualizar el ángulo basado en la dirección
        angle += (clockwise ? 1 : -1) * speed * Time.deltaTime;

        // Calcular la nueva posición en la órbita circular
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        // Actualizar la posición del jugador
        transform.position = new Vector3(x, y, 0);
    }
}
