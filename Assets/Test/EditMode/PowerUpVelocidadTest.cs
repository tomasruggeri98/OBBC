using NUnit.Framework;
using UnityEngine;

public class PowerUpVelocidadTest
{
    [Test]
    public void PlayerTakesDamageReducesHealthCorrectly()
    {
        // Configuración de prueba
        var player = new GameObject().AddComponent<PlayerHealth>();
        player.maxHealth = 3; 
        player.projectileDamage = 1;

        // Crear un GameObject para representar el texto de la salud y asignarlo al jugador
        var healthTextObject = new GameObject("HealthText");
        var healthText = healthTextObject.AddComponent<UnityEngine.UI.Text>();
        player.healthText = healthText;

        // Inicializar la salud
        player.Start();
        var initialHealth = player.GetCurrentHealth();

        // Invocar la función TakeDamage
        player.TakeDamage(player.projectileDamage);

        // Verificar que la salud disminuyó correctamente
        Assert.AreEqual(initialHealth - player.projectileDamage, player.GetCurrentHealth(), "La salud no se redujo correctamente al aplicar daño.");
    }
}
