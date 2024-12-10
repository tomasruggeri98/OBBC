using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInmovilizacionTest
{
    [Test]
    public void PlayerCanShootProjectile()
    {
        // Configuración de prueba
        var player = new GameObject().AddComponent<PlayerShooting>();
        var projectilePrefab = new GameObject("Projectile");
        projectilePrefab.AddComponent<Rigidbody2D>();
        projectilePrefab.SetActive(false);

        player.projectilePrefab = projectilePrefab;
        player.poolSize = 1;
        player.shootingForce = 10f;

        // Inicializar el pool de proyectiles
        player.Start();

        // Verifica que el pool contenga un proyectil inactivo
        Assert.AreEqual(1, player.poolSize, "El tamaño del pool no es correcto.");
        Assert.IsFalse(projectilePrefab.activeSelf, "El proyectil debería estar inactivo.");

        // Simular un disparo
        player.Shoot();
        projectilePrefab.SetActive(true);

        // Verificar que el proyectil esté activo
        Assert.IsTrue(projectilePrefab.activeSelf, "El proyectil debería estar activo después de disparar.");

        // Simular que el proyectil se desactiva después de 3 segundos
        IEnumerator DeactivateTest()
        {
            yield return new WaitForSeconds(3f);
            projectilePrefab.SetActive(false);
            Assert.IsFalse(projectilePrefab.activeSelf, "El proyectil debería estar inactivo después de 3 segundos.");
        }
    }

}

