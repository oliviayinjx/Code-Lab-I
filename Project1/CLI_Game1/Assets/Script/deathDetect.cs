using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathDetect : MonoBehaviour
{
    //this is attached to the air wall at the bottom of level
    //when player falls from the platforms and hit this
    //level will restart
    private GameManager gameManager; 

    void Start()
    {
        //find the game manager
        gameManager = GameManager.findInstance();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player collides with this trigger
        //level restarted
        if (other.gameObject.tag == "Player")
        {
            GameManager.reloadCurrent();
        }
    }

}
