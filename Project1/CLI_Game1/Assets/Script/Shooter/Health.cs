using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] bool isPlayer;
    public GameObject gameDesigner; 
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

        //if player collides with win object
        if(gameObject.name == "Player" & other.gameObject.layer == 10)
        {
            winCondition();
        }
    }

    void TakeDamage(int damage)
    {
        //take damage
        health -= damage;

        if (health <= 0)
        {
            //if this gameObject runs out of health, get destroy
            Die();
        }
        else
        {

        }
    }

    void Die()
    {
        //if player dies, restart the level
        if (!isPlayer && gameObject.name == "Player")
        {
            if(SceneManager.GetActiveScene().name == "End")
            {
                gameDesigner.GetComponent <SpriteRenderer>().enabled = true; 
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
        Destroy(gameObject);
    }

    void winCondition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
