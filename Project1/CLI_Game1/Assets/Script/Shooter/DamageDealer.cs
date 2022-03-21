using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //hold amount of damage
    [SerializeField] int damage = 10;
    //getter to get damage
    public int GetDamage()
    {
        return damage; 
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
