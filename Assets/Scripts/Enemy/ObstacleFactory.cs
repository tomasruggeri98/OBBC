using UnityEngine;

public class ObstacleFactory : IObjectFactory
{
    private GameObject prefab;

    public ObstacleFactory(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(prefab, position, rotation);
    }
}
