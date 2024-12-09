using NUnit.Framework;
using UnityEngine;

public class PowerUpVelocidadTest
{
    [Test]
    public void PowerUpAumentaVelocidadDelJugador()
    {
        // Configuración de prueba
        var player = new GameObject().AddComponent<PlayerMovement>();
        var powerUp = new GameObject().AddComponent<PowerUpVelocidad>();

        // Asignar el movimiento y la referencia del power-up
        powerUp.playerMovement = player;
        powerUp.velocidadAumentada = 100f;
        powerUp.cargador = 100f;

        // Activar el power-up
        powerUp.ActivarPowerUp();

        // Verificar que la velocidad ha aumentado
        Assert.AreEqual(100f, player.speed, "La velocidad del jugador no aumentó cuando el power-up está activo.");

        // Verificar que el jugador está inmovilizado
        Assert.IsTrue(player.invomilizacion, "El jugador no está inmovilizado durante el power-up.");
    }
}
