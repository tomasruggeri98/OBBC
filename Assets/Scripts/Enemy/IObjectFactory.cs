using UnityEngine;

public interface IObjectFactory
{
    GameObject CreateObject(Vector3 position, Quaternion rotation);
}
