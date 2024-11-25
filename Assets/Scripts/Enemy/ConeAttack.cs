using UnityEngine;

[System.Serializable]
public class ConeAttack : IAttack
{
    [Header("Ataque de Cono")]
    public float radius = 1f; // Distancia máxima del ataque del cono
    public float angleRange = 45f; // Ángulo del cono
    public float attackInterval = 5f; // Intervalo entre ataques del cono

    private IObjectFactory coneFactory;

    public ConeAttack(IObjectFactory factory)
    {
        this.coneFactory = factory;
    }

    public void ExecuteAttack(EnemyTest enemy)
    {
        // Determinar la dirección del ataque
        float attackDirection = Random.Range(0f, 360f);

        // La posición del cono será la misma que la del enemigo, ya que el vértice del cono está en el centro
        Vector2 spawnPosition = enemy.transform.position;

        // Crear el cono en la posición del enemigo
        GameObject cone = coneFactory.CreateObject(spawnPosition, Quaternion.identity);

        // Rotar el cono para que su vértice apunte hacia afuera en la dirección deseada
        cone.transform.rotation = Quaternion.Euler(0f, 0f, attackDirection);

        // Configurar el cono para extenderse desde el enemigo hacia afuera
        // Aquí asumimos que el prefab del cono tiene su vértice en (0, 0) y se expande hacia el eje positivo local Y

        // Destruir el cono después de un tiempo
        Object.Destroy(cone, 5f);
    }
}
