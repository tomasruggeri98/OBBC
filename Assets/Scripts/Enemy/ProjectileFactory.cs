using UnityEngine;

public class ProjectileFactory : IObjectFactory
{
    private GameObject prefab;

    public ProjectileFactory(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(prefab, position, rotation);
    }
}
