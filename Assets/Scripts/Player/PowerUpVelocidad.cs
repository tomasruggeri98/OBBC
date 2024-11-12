using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpVelocidad : MonoBehaviour
{
    // Variables de cargador y estado del power-up
    public float cargador = 100f;
    public float maxCargador = 100f;
    public float velocidadAumentada = 100f; // Velocidad durante el power-up
    public float consumoPorSegundo = 10f;
    public float velocidadRecarga = 5f;
    public float tiempoEsperaRecarga = 2f;
    private bool powerUpActivo = false;
    private bool enRecarga = false;

    // Referencias
    public PlayerMovement playerMovement; // Asigna tu script PlayerMovement
    [SerializeField] private Slider barraCargador;
    private Collider2D playerCollider;

    void Start()
    {
        // Aseg�rate de que el cargador est� completo al inicio y obtener el collider
        cargador = maxCargador;
        playerCollider = GetComponent<Collider2D>();

        if (barraCargador != null)
            barraCargador.value = cargador / maxCargador;
    }

    void Update()
    {
        // Mostrar el estado del cargador en la barra de UI
        if (barraCargador != null)
            barraCargador.value = cargador / maxCargador;

        // Activar el power-up cuando se presiona la tecla A y el cargador tiene energ�a
        if (Input.GetKeyDown(KeyCode.A) && cargador > 0 && !enRecarga)
        {
            ActivarPowerUp();
        }

        // Desactivar el power-up cuando el cargador se vac�e
        if (powerUpActivo && cargador <= 0)
        {
            DesactivarPowerUp();
            StartCoroutine(RecargarCargador());
        }
    }

    private void FixedUpdate()
    {
        // Consumir energ�a del cargador mientras el power-up est� activo
        if (powerUpActivo)
        {
            cargador -= consumoPorSegundo * Time.deltaTime;
            cargador = Mathf.Clamp(cargador, 0, maxCargador);
        }
    }

    // M�todo para activar el power-up
    private void ActivarPowerUp()
    {
        powerUpActivo = true;
        playerMovement.speed = velocidadAumentada; // Aumenta la velocidad en PlayerMovement

        // Desactivar cambio de direcci�n
        playerMovement.invomilizacion = true;

        // Hacer al jugador invulnerable desactivando el collider
        playerCollider.enabled = false;
    }

    // M�todo para desactivar el power-up
    private void DesactivarPowerUp()
    {
        powerUpActivo = false;
        playerMovement.speed = 50f; // Restaura la velocidad original

        // Restaurar la habilidad de cambiar de direcci�n
        playerMovement.invomilizacion = false;

        // Hacer al jugador vulnerable nuevamente activando el collider
        playerCollider.enabled = true;
    }

    // Corutina para recargar el cargador despu�s de que se agote
    private IEnumerator RecargarCargador()
    {
        enRecarga = true;
        yield return new WaitForSeconds(tiempoEsperaRecarga); // Espera antes de comenzar a recargar

        while (cargador < maxCargador)
        {
            cargador += velocidadRecarga * Time.deltaTime;
            cargador = Mathf.Clamp(cargador, 0, maxCargador);
            yield return null;
        }

        enRecarga = false;
    }
}