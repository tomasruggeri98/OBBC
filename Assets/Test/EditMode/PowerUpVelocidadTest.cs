using NUnit.Framework;
using UnityEngine;

public class PowerUpVelocidadTest
{
    [Test]
    public void PowerUpAumentaVelocidadDelJugador()
    {
        // Configuraci�n de prueba
        var player = new GameObject().AddComponent<PlayerMovement>();
        var powerUp = new GameObject().AddComponent<PowerUpVelocidad>();

        // Asignar el movimiento y la referencia del power-up
        powerUp.playerMovement = player;
        powerUp.velocidadAumentada = 100f;
        powerUp.cargador = 100f;

        // Activar el power-up
        powerUp.ActivarPowerUp();

        // Verificar que la velocidad ha aumentado
        Assert.AreEqual(100f, player.speed, "La velocidad del jugador no aument� cuando el power-up est� activo.");

        // Verificar que el jugador est� inmovilizado
        Assert.IsTrue(player.invomilizacion, "El jugador no est� inmovilizado durante el power-up.");
    }
}
