using NUnit.Framework;
using UnityEngine;

public class PlayerInmovilizacionTest
{
    [Test]
    public void JugadorNoPuedeCambiarDeDireccionCuandoEstaInmovilizado()
    {
        // Crear un objeto para el jugador y añadir el componente PlayerMovement
        var player = new GameObject().AddComponent<PlayerMovement>();

        // Crear un objeto para el power-up y asignar el componente PowerUpVelocidad
        var powerUp = new GameObject().AddComponent<PowerUpVelocidad>();

        // Asignar playerMovement al power-up
        powerUp.playerMovement = player;

        // Activar el power-up
        powerUp.ActivarPowerUp();

        // Verificar que el jugador está inmovilizado
        Assert.IsTrue(player.invomilizacion, "El jugador no está inmovilizado cuando se activa el power-up.");

        // Intentar cambiar la dirección (esto debería fallar si está inmovilizado)
        bool initialClockwise = player.clockwise;
        player.OnChangeDirection(new UnityEngine.InputSystem.InputAction.CallbackContext());

        // Verificar que la dirección no cambió
        Assert.AreEqual(initialClockwise, player.clockwise, "El jugador cambió de dirección cuando debería estar inmovilizado.");
    }

}
