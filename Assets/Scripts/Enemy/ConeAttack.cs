using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConeAttack : IAttack
{
    [Header("Ataque de Cono")]
    public GameObject conePrefab; // Prefab del cono
    public float radius = 5f; // Radio del círculo donde aparecerá el cono
    public float angleRange = 45f; // Ángulo del cono
    public float attackInterval = 5f; // Intervalo entre cada ataque del cono

    public void ExecuteAttack(EnemyTest enemy)
    {
        float angle = Random.Range(0f, 360f);
        Vector2 spawnPosition = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * 5f,
            Mathf.Sin(angle * Mathf.Deg2Rad) * 5f
        );

        GameObject cone = Object.Instantiate(conePrefab, spawnPosition, Quaternion.identity);
        float randomRotation = Random.Range(0f, 360f);
        cone.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        Object.Destroy(cone, 5f);
    }
}