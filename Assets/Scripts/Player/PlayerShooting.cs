using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public float shootingForce = 10f; // Fuerza de disparo
    public float shootingInterval = 0.5f; // Intervalo de disparo en segundos
    public int poolSize = 10; // Tama�o del pool

    private Queue<GameObject> projectilePool; // Cola de proyectiles

    public void Start()
    {
        // Crear el pool de proyectiles
        InitializeProjectilePool();

        // Inicia el disparo autom�tico
        StartCoroutine(AutoShoot());
    }

    private void InitializeProjectilePool()
    {
        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false); // Desactiva inicialmente el proyectil
            projectilePool.Enqueue(projectile); // A�ade al pool
        }
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootingInterval); // Espera el intervalo antes de disparar nuevamente
        }
    }

    public void Shoot()
    {
        if (projectilePool.Count > 0)
        {
            // Obtener un proyectil del pool
            GameObject projectile = projectilePool.Dequeue();
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true); // Activar el proyectil

            // Calcular la direcci�n hacia el centro (ajusta seg�n tu l�gica de direcci�n)
            Vector2 direction = Vector2.zero - (Vector2)transform.position;
            direction.Normalize();

            // Aplicar la fuerza en la direcci�n
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * shootingForce;
            }

            // Reutilizar el proyectil despu�s de 3 segundos
            StartCoroutine(DeactivateProjectileAfterTime(projectile, 3f));
        }
    }

    private IEnumerator DeactivateProjectileAfterTime(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile); // Regresa el proyectil al pool
    }
}
