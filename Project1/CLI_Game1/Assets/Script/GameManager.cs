using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    //making score static to make sure there is only one score in the game
    public static float score;
    private float highScore;

    public float HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value; 
        }
    }

    private static GameManager instance;

    public static GameManager findInstance()
    {
        return instance; 
    }
    //singleton 
    private void Awake()
    {
        //when there is a instance and this new spawn is not that instance
        //destory this new spawn
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else //otherwise spawn a new instance 
        {
            instance = this;
            //when load new scene don't destory the game manager 
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
            Debug.Log("High Score" + highScore);
        }
    }

    public static void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void reloadCurrent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}