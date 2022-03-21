using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    //damage dealer and reduce health by damage amount

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        //if damageDealer has data stored
        if (damageDealer != null)
        {
            //Take Damage
            TakeDamage(damageDealer.GetDamage());

            //Tell damage dealer that it hit something
            damageDealer.Hit();
            
        }
    }

    void TakeDamage(int damage)
    {
        //take damage
        health -= damage;

        if (health <= 0)
        {
            //if this gameObject runs out of health, get destroy
            Destroy(gameObject);
        }
    }
}
