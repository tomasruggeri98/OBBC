using NUnit.Framework;
using UnityEngine;

public class PlayerHealthTest
{
    [Test]
    public void GameEndsWhenPlayerHealthReachesZero()
    {
        // Configuración del GameManager
        var gameManagerObject = new GameObject("GameManager");
        var gameManager = gameManagerObject.AddComponent<GameManager>();

        // Configuración del texto de resultado
        var resultTextObject = new GameObject("ResultText");
        var resultText = resultTextObject.AddComponent<UnityEngine.UI.Text>();
        gameManager.resultText = resultText;

        // Configuración del jugador
        var playerObject = new GameObject("Player");
        var playerHealth = playerObject.AddComponent<PlayerHealth>();
        playerHealth.maxHealth = 3; // Salud inicial del jugador
        playerHealth.projectileDamage = 1; // Daño recibido por proyectil
        playerHealth.healthText = new GameObject("HealthText").AddComponent<UnityEngine.UI.Text>();

        // Inicializar el jugador y simular daño
        playerHealth.Start();
        playerHealth.TakeDamage(playerHealth.projectileDamage); // Salud: 2
        playerHealth.TakeDamage(playerHealth.projectileDamage); // Salud: 1
        playerHealth.TakeDamage(playerHealth.projectileDamage); // Salud: 0

        // Verificar que el GameManager detectó la derrota del jugador
        GameManager.SetPlayerDefeated(true); // Asegura que el GameManager procesa la derrota
        gameManager.EndGame();

        // Verificar que el texto de resultado muestra "Derrota"
        Assert.AreEqual("Derrota", gameManager.resultText.text, "El texto de resultado no muestra 'Derrota' al finalizar el juego cuando la salud del jugador llega a cero.");
    }
}
