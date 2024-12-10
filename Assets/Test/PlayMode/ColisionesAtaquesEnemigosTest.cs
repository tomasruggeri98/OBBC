using NUnit.Framework;
using UnityEngine;

public class PlayerHealthTests
{
    private GameObject player;
    private PlayerHealth playerHealth;

    private GameObject projectileEnemy;
    private GameObject obstacleAttack;
    private GameObject zoneAttack;

    [SetUp]
    public void SetUp()
    {
        // Crear el jugador y agregarle el componente PlayerHealth
        player = new GameObject("Player");
        playerHealth = player.AddComponent<PlayerHealth>();
        playerHealth.maxHealth = 5;  // Configurar salud máxima
        playerHealth.projectileDamage = 1;  // Daño del proyectil
        playerHealth.obstacleDamage = 1;  // Daño del obstáculo
        playerHealth.zoneDamage = 1;  // Daño de la zona

        var healthTextObject = new GameObject("HealthText");
        var healthText = healthTextObject.AddComponent<UnityEngine.UI.Text>();
        playerHealth.healthText = healthText;

        // Crear el proyectil enemigo
        projectileEnemy = new GameObject("ProjectileEnemy");
        projectileEnemy.tag = "ProjectileEnemy";  // Etiqueta del proyectil
        var projectileCollider = projectileEnemy.AddComponent<BoxCollider2D>();
        projectileCollider.isTrigger = true;  // Hacer trigger el collider
        projectileEnemy.AddComponent<Rigidbody2D>().isKinematic = true;  // Evitar que la física afecte el proyectil

        // Crear el obstáculo
        obstacleAttack = new GameObject("ObstacleAttack");
        obstacleAttack.tag = "ObstaculeAttack";  // Etiqueta del obstáculo
        var obstacleCollider = obstacleAttack.AddComponent<BoxCollider2D>();
        obstacleCollider.isTrigger = true;  // Hacer trigger el collider

        // Crear la zona de daño
        zoneAttack = new GameObject("ZoneAttack");
        zoneAttack.tag = "ZoneAttack";  // Etiqueta de la zona
        var zoneCollider = zoneAttack.AddComponent<BoxCollider2D>();
        zoneCollider.isTrigger = true;  // Hacer trigger el collider

        playerHealth.Start();
        playerHealth.UpdateHealthText();

    }

    [Test]
    public void PlayerTakesDamageWhenHitByProjectile()
    {
        // Salud inicial del jugador
        int initialHealth = playerHealth.GetCurrentHealth();

        // Simular la colisión del proyectil con el jugador
        playerHealth.OnTriggerEnter2D(projectileEnemy.GetComponent<Collider2D>());
        playerHealth.UpdateHealthText();

        // Verificar que la salud del jugador disminuyó correctamente
        Assert.AreEqual(initialHealth - playerHealth.projectileDamage, playerHealth.GetCurrentHealth(),
            "El jugador no recibió el daño correcto del proyectil.");
    }

    [Test]
    public void PlayerTakesDamageWhenEnteringObstacle()
    {
        // Salud inicial del jugador
        int initialHealth = playerHealth.GetCurrentHealth();

        // Simular la colisión con el obstáculo
        playerHealth.OnTriggerEnter2D(obstacleAttack.GetComponent<Collider2D>());
        playerHealth.UpdateHealthText();

        // Verificar que la salud del jugador disminuyó correctamente
        Assert.AreEqual(initialHealth - playerHealth.obstacleDamage, playerHealth.GetCurrentHealth(),
            "El jugador no recibió el daño correcto del obstáculo.");
    }

    [Test]
    public void PlayerTakesDamageWhenEnteringZone()
    {
        // Salud inicial del jugador
        int initialHealth = playerHealth.GetCurrentHealth();

        // Simular la colisión con la zona
        playerHealth.OnTriggerEnter2D(zoneAttack.GetComponent<Collider2D>());
        playerHealth.UpdateHealthText();

        // Verificar que la salud del jugador disminuyó correctamente
        Assert.AreEqual(initialHealth - playerHealth.zoneDamage, playerHealth.GetCurrentHealth(),
            "El jugador no recibió el daño correcto de la zona.");
    }

    [TearDown]
    public void TearDown()
    {
        // Limpiar los objetos creados en el test
        Object.Destroy(player);
        Object.Destroy(projectileEnemy);
        Object.Destroy(obstacleAttack);
        Object.Destroy(zoneAttack);
    }
}
