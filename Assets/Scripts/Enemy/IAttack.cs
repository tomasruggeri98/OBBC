using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void ExecuteAttack(EnemyTest enemy); // Pasamos el enemigo para acceder a sus propiedades
}