using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab del objeto (proyectil)
    public int poolSize = 10; // Tamaño del pool
    private List<GameObject> pool; // Lista de objetos en el pool

    private void Awake()
    {
        // Inicializar la pool
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // Desactivar el objeto
            pool.Add(obj);
        }
    }

    // Método para obtener un objeto de la pool
    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null; // Si no hay objetos disponibles
    }

    // Método para regresar un objeto a la pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
