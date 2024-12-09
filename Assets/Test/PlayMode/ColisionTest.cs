using NUnit.Framework;
using UnityEngine;

public class ColisionTest
{
    private GameObject enemy;
    private EnemyTest enemyTest;
    private GameObject projectile;

    [SetUp]
    public void SetUp()
    {
        // Crear el enemigo y proyectil en la escena
        enemy = new GameObject("Enemy");
        enemyTest = enemy.AddComponent<EnemyTest>();
        enemyTest.maxHealth = 1;  // Configuramos la salud máxima
        enemyTest.currentHealth = 1;
        
        // Crear el proyectil
        projectile = new GameObject("Projectile");
        projectile.tag = "Projectile";  // Aseguramos que tenga la etiqueta correcta
        projectile.AddComponent<Rigidbody2D>().isKinematic = true;  // Agregar Rigidbody2D
        projectile.AddComponent<BoxCollider2D>().isTrigger = true;  // Hacer que el collider sea trigger

        // Asegurarnos de que el enemigo tenga un collider
        if (enemy.GetComponent<Collider2D>() == null)
        {
            enemy.AddComponent<BoxCollider2D>().isTrigger = true;
        }

        // Verificar la salud inicial antes de la colisión
        Debug.Log("Salud inicial del enemigo: " + enemyTest.maxHealth);
    }

    [Test]
    public void EnemyTakesDamageWhenCollidesWithProjectile()
    {
        // Obtener la salud inicial del enemigo
        int initialHealth = enemyTest.maxHealth;
        Debug.Log("Salud antes de la colisión: " + enemyTest.currentHealth);

        // Simulamos la colisión
        enemyTest.OnTriggerEnter2D(projectile.GetComponent<Collider2D>());

        // Verificar que la vida del enemigo haya disminuido en 1
        Debug.Log("Salud después de la colisión: " + enemyTest.currentHealth);

        Assert.AreEqual(initialHealth - 1, enemyTest.currentHealth);
    }
}
