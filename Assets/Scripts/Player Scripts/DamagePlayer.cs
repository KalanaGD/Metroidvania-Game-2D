using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int _damageAmount = 1 ;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            DealDamage();
        }
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    public void DealDamage()
    {
        PlayerHealthManager.instance.DamagePlayer( _damageAmount);
    }


}
