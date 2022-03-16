using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
    /*
     * this code is on colliectibles
     */
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //find the game manager
        gameManager = GameManager.findInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player picks up the collectible, the collect numebr in game manager add 1
        //then this colletible is destroyed 
       if(collision.gameObject.tag == "Player")
        {
            gameManager.Collect ++;
            Destroy(gameObject);
        }
    }
}
