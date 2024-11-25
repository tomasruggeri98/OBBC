using UnityEngine;

[System.Serializable]
public class ConeAttack : IAttack
{
    [Header("Ataque de Cono")]
    public float radius = 1f; // Distancia m�xima del ataque del cono
    public float angleRange = 45f; // �ngulo del cono
    public float attackInterval = 5f; // Intervalo entre ataques del cono

    private IObjectFactory coneFactory;

    public ConeAttack(IObjectFactory factory)
    {
        this.coneFactory = factory;
    }

    public void ExecuteAttack(EnemyTest enemy)
    {
        // Determinar la direcci�n del ataque
        float attackDirection = Random.Range(0f, 360f);

        // La posici�n del cono ser� la misma que la del enemigo, ya que el v�rtice del cono est� en el centro
        Vector2 spawnPosition = enemy.transform.position;

        // Crear el cono en la posici�n del enemigo
        GameObject cone = coneFactory.CreateObject(spawnPosition, Quaternion.identity);

        // Rotar el cono para que su v�rtice apunte hacia afuera en la direcci�n deseada
        cone.transform.rotation = Quaternion.Euler(0f, 0f, attackDirection);

        // Configurar el cono para extenderse desde el enemigo hacia afuera
        // Aqu� asumimos que el prefab del cono tiene su v�rtice en (0, 0) y se expande hacia el eje positivo local Y

        // Destruir el cono despu�s de un tiempo
        Object.Destroy(cone, 5f);
    }
}
