using UnityEngine;

public class ConeFactory : IObjectFactory
{
    private GameObject prefab;

    public ConeFactory(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(prefab, position, rotation);
    }
}
