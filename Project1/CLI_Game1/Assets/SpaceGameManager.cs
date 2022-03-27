using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceGameManager : MonoBehaviour
{
    private void Awake()
    {
        ////destory platformer game manager in shooting game
        //if (SceneManager.GetActiveScene().name == "Transition")
        //{
        //    Destroy(GameObject.Find("GameManager"));
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void loadNextScene()
    {
        //load next level in the order of build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
