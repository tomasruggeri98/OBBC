using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marcar la clase como serializable
[System.Serializable]
public class ShootAtPlayerAttack : IAttack
{
    [Header("Disparo al Jugador")]
    public float shootingInterval = 2f; // Intervalo de disparo en segundos
    public float projectileSpeed = 5f; // Velocidad del proyectil
    public GameObject projectilePrefab; // Prefab del proyectil

    private Transform player; // Referencia al jugador

    public void ExecuteAttack(EnemyTest enemy)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Disparo cada cierto intervalo
        if (player == null) return;

        Vector2 direction = (player.position - enemy.transform.position).normalized;
        GameObject projectile = Object.Instantiate(projectilePrefab, enemy.transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
        Object.Destroy(projectile, 4f);
    }
}