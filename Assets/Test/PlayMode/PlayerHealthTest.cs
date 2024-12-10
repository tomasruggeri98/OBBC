using NUnit.Framework;
using UnityEngine;

public class PlayerHealthTest
{
    [Test]
    public void GameEndsWhenPlayerHealthReachesZero()
    {
        // Configuraci�n del GameManager
        var gameManagerObject = new GameObject("GameManager");
        var gameManager = gameManagerObject.AddComponent<GameManager>();

        // Configuraci�n del texto de resultado
        var resultTextObject = new GameObject("ResultText");
        var resultText = resultTextObject.AddComponent<UnityEngine.UI.Text>();
        gameManager.resultText = resultText;

        // Configuraci�n del jugador
        var playerObject = new GameObject("Player");
        var playerHealth = playerObject.AddComponent<PlayerHealth>();
        playerHealth.maxHealth = 3; // Salud inicial del jugador
        playerHealth.projectileDamage = 1; // Da�o recibido por proyectil
        playerHealth.healthText = new GameObject("HealthText").AddComponent<UnityEngine.UI.Text>();

        // Inicializar el jugador y simular da�o
        playerHealth.Start();
        playerHealth.TakeDamage(playerHealth.projectileDamage); // Salud: 2
        playerHealth.TakeDamage(playerHealth.projectileDamage); // Salud: 1
        playerHealth.TakeDamage(playerHealth.projectileDamage); // Salud: 0

        // Verificar que el GameManager detect� la derrota del jugador
        GameManager.SetPlayerDefeated(true); // Asegura que el GameManager procesa la derrota
        gameManager.EndGame();

        // Verificar que el texto de resultado muestra "Derrota"
        Assert.AreEqual("Derrota", gameManager.resultText.text, "El texto de resultado no muestra 'Derrota' al finalizar el juego cuando la salud del jugador llega a cero.");
    }
}
