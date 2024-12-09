using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    // Input System
    private PlayerInput controls;

    private void Awake()
    {
        // Inicializar controles
        controls = new PlayerInput();
        controls.Player.ActivatePowerUp.performed += OnActivatePowerUp;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        // Asegúrate de que el cargador esté completo al inicio y obtener el collider
        cargador = maxCargador;
        playerCollider = GetComponent<Collider2D>();

        if (barraCargador != null)
            barraCargador.value = cargador / maxCargador;
    }

    private void Update()
    {
        // Mostrar el estado del cargador en la barra de UI
        if (barraCargador != null)
            barraCargador.value = cargador / maxCargador;

        // Desactivar el power-up cuando el cargador se vacíe
        if (powerUpActivo && cargador <= 0)
        {
            DesactivarPowerUp();
            StartCoroutine(RecargarCargador());
        }
    }

    private void FixedUpdate()
    {
        // Consumir energía del cargador mientras el power-up esté activo
        if (powerUpActivo)
        {
            cargador -= consumoPorSegundo * Time.deltaTime;
            cargador = Mathf.Clamp(cargador, 0, maxCargador);
        }
    }

    private void OnActivatePowerUp(InputAction.CallbackContext context)
    {
        // Activar el power-up cuando se presiona la acción y el cargador tiene energía
        if (cargador > 0 && !enRecarga)
        {
            ActivarPowerUp();
        }
    }

    // Método para activar el power-up
    public void ActivarPowerUp()
    {
        if (playerMovement == null)
        {
            Debug.LogError("playerMovement no está asignado en ActivarPowerUp");
            return;
        }
        powerUpActivo = true;
        playerMovement.speed = velocidadAumentada; // Aumenta la velocidad en PlayerMovement

        // Desactivar cambio de dirección
        playerMovement.invomilizacion = true;

        // Hacer al jugador invulnerable desactivando el collider
        playerCollider.enabled = false;
    }

    // Método para desactivar el power-up
    private void DesactivarPowerUp()
    {
        powerUpActivo = false;
        playerMovement.speed = 50f; // Restaura la velocidad original

        // Restaurar la habilidad de cambiar de dirección
        playerMovement.invomilizacion = false;

        // Hacer al jugador vulnerable nuevamente activando el collider
        playerCollider.enabled = true;
    }

    // Corutina para recargar el cargador después de que se agote
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
