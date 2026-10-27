using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int _maxHealth = 3;

    public void DamageEnemy(int _damageAmount)
    {
        _maxHealth -= _damageAmount;

        if(_maxHealth <= 0)
        {
            Destroy(gameObject);
            // damge FX
        }
    }


}
