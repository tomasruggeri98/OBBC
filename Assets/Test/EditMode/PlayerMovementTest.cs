using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTest
{
    [Test]
    public void PlayerMovesWhenGameStarts()
    {
        // Configuraci�n de prueba
        var player = new GameObject().AddComponent<PlayerMovement>();

        // Verifica que el jugador tiene un componente de movimiento
        Assert.IsNotNull(player, "El jugador no tiene el componente de movimiento.");

        // Simula una actualizaci�n del juego
        Vector3 initialPosition = player.transform.position;
        player.Update();

        // Verifica que la posici�n cambi�
        Assert.AreNotEqual(initialPosition, player.transform.position, "El jugador no se movi�.");
    }
}
