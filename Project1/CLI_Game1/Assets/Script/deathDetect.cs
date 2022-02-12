using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathDetect : MonoBehaviour
{
    private GameManager gameManager; 

    void Start()
    {
        gameManager = GameManager.findInstance();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.reloadCurrent();
        }
    }

}
