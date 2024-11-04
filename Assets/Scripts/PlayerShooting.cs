using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public float shootingForce = 10f; // Fuerza de disparo
    public float shootingInterval = 0.5f; // Intervalo de disparo en segundos

    private void Start()
    {
        // Inicia el disparo autom�tico
        StartCoroutine(AutoShoot());
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootingInterval); // Espera el intervalo antes de disparar nuevamente
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil en la posici�n del jugador
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Calcular la direcci�n hacia el centro (ajusta seg�n tu l�gica de direcci�n)
        Vector2 direction = Vector2.zero - (Vector2)transform.position;
        direction.Normalize();

        // Aplicar la fuerza en la direcci�n
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * shootingForce;
        }

        // Destruir el proyectil despu�s de 3 segundos (ajusta si es necesario)
        Destroy(projectile, 3f);
    }
}
