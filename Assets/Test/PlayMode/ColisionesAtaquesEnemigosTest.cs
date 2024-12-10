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
        playerHealth.maxHealth = 5;  // Configurar salud m�xima
        playerHealth.projectileDamage = 1;  // Da�o del proyectil
        playerHealth.obstacleDamage = 1;  // Da�o del obst�culo
        playerHealth.zoneDamage = 1;  // Da�o de la zona

        var healthTextObject = new GameObject("HealthText");
        var healthText = healthTextObject.AddComponent<UnityEngine.UI.Text>();
        playerHealth.healthText = healthText;

        // Crear el proyectil enemigo
        projectileEnemy = new GameObject("ProjectileEnemy");
        projectileEnemy.tag = "ProjectileEnemy";  // Etiqueta del proyectil
        var projectileCollider = projectileEnemy.AddComponent<BoxCollider2D>();
        projectileCollider.isTrigger = true;  // Hacer trigger el collider
        projectileEnemy.AddComponent<Rigidbody2D>().isKinematic = true;  // Evitar que la f�sica afecte el proyectil

        // Crear el obst�culo
        obstacleAttack = new GameObject("ObstacleAttack");
        obstacleAttack.tag = "ObstaculeAttack";  // Etiqueta del obst�culo
        var obstacleCollider = obstacleAttack.AddComponent<BoxCollider2D>();
        obstacleCollider.isTrigger = true;  // Hacer trigger el collider

        // Crear la zona de da�o
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

        // Simular la colisi�n del proyectil con el jugador
        playerHealth.OnTriggerEnter2D(projectileEnemy.GetComponent<Collider2D>());
        playerHealth.UpdateHealthText();

        // Verificar que la salud del jugador disminuy� correctamente
        Assert.AreEqual(initialHealth - playerHealth.projectileDamage, playerHealth.GetCurrentHealth(),
            "El jugador no recibi� el da�o correcto del proyectil.");
    }

    [Test]
    public void PlayerTakesDamageWhenEnteringObstacle()
    {
        // Salud inicial del jugador
        int initialHealth = playerHealth.GetCurrentHealth();

        // Simular la colisi�n con el obst�culo
        playerHealth.OnTriggerEnter2D(obstacleAttack.GetComponent<Collider2D>());
        playerHealth.UpdateHealthText();

        // Verificar que la salud del jugador disminuy� correctamente
        Assert.AreEqual(initialHealth - playerHealth.obstacleDamage, playerHealth.GetCurrentHealth(),
            "El jugador no recibi� el da�o correcto del obst�culo.");
    }

    [Test]
    public void PlayerTakesDamageWhenEnteringZone()
    {
        // Salud inicial del jugador
        int initialHealth = playerHealth.GetCurrentHealth();

        // Simular la colisi�n con la zona
        playerHealth.OnTriggerEnter2D(zoneAttack.GetComponent<Collider2D>());
        playerHealth.UpdateHealthText();

        // Verificar que la salud del jugador disminuy� correctamente
        Assert.AreEqual(initialHealth - playerHealth.zoneDamage, playerHealth.GetCurrentHealth(),
            "El jugador no recibi� el da�o correcto de la zona.");
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
