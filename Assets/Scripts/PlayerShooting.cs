using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public ObjectPool projectilePool; // Pool de proyectiles
    public float shootingForce = 10f; // Fuerza de disparo
    public int maxShots = 5; // Máxima cantidad de disparos permitidos
    private int currentShots;

    private void Start()
    {
        currentShots = maxShots;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && currentShots > 0)
        {
            Shoot();
            currentShots--;
            Invoke("ReloadShot", 3f); // Recargar un disparo cada 3 segundos
        }
    }

    void Shoot()
    {
        // Obtener un proyectil de la pool
        GameObject projectile = projectilePool.GetObject();
        if (projectile != null)
        {
            // Posicionar el proyectil en la posición del jugador
            projectile.transform.position = transform.position;

            // Calcular la dirección hacia el centro
            Vector2 direction = Vector2.zero - (Vector2)transform.position;
            direction.Normalize();

            // Aplicar la fuerza en la dirección
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * shootingForce;

            // Iniciar la corutina para regresar el proyectil a la pool después de 3 segundos
            StartCoroutine(ReturnProjectileToPoolAfterDelay(projectile, 3f));
        }
    }

    void ReloadShot()
    {
        if (currentShots < maxShots)
        {
            currentShots++;
        }
    }

    // Corutina para regresar el proyectil a la pool después de un retraso
    IEnumerator ReturnProjectileToPoolAfterDelay(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        projectilePool.ReturnObject(projectile);
    }
}
