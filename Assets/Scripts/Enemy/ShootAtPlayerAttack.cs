using UnityEngine;

[System.Serializable]
public class ShootAtPlayerAttack : IAttack
{
    [Header("Disparo al Jugador")]
    public float shootingInterval = 2f;
    public float projectileSpeed = 5f;

    private IObjectFactory projectileFactory;
    private Transform player;

    public ShootAtPlayerAttack(IObjectFactory factory)
    {
        this.projectileFactory = factory;
    }

    public void ExecuteAttack(EnemyTest enemy)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null) return;

        Vector2 direction = (player.position - enemy.transform.position).normalized;
        GameObject projectile = projectileFactory.CreateObject(enemy.transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
        Object.Destroy(projectile, 4f);
    }
}
