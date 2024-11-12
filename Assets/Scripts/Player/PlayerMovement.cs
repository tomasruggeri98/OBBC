using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 3f; // Radio del c�rculo
    public float speed = 50f; // Velocidad de movimiento
    private float angle = 0f; // �ngulo inicial
    private bool clockwise = true; // Direcci�n de movimiento
    public bool invomilizacion;

    void Update()
    {
        MovementPlayer();
    }
    void MovementPlayer()
    {
        // Cambiar direcci�n al presionar la tecla "A"
        if (Input.GetKeyDown(KeyCode.A) && !invomilizacion)
        {
            clockwise = !clockwise; // Cambiar la direcci�n
        }

        // Actualizar el �ngulo basado en la direcci�n
        angle += (clockwise ? 1 : -1) * speed * Time.deltaTime;

        // Calcular la nueva posici�n en la �rbita circular
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        // Actualizar la posici�n del jugador
        transform.position = new Vector3(x, y, 0);
    }
}
