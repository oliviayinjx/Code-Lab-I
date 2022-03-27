using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    //making score static to make sure there is only one score in the game
    public static float score; //level player reach
    private static int collect; //how many collectables player gets
    private static int startCollect; //sum of last levels which is starting number of next level
    private float highScore; //highest level player reach

    //getter and setter of highscore
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
    //getter and setter of collectables
    public int Collect
    {
        get
        {
            return collect;
        }
        set
        {
            collect = value; 
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
            //if socre is greater than highScore
            //replace highscore with current score
            highScore = score;
            Debug.Log("High Score" + highScore);
        }
    }

    public static void loadNextScene()
    {
        //load next level in the order of build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //store collectiable at the begin of each level
        startCollect = collect;
        Debug.Log("Collectible: " + collect);
    }

    public static void reloadCurrent()
    {
        //reload current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //reload level, and collect numbers back to orgin state, avoid repeat
        collect = startCollect;
    }
    
        public static void shortCut()
    {
        //reload transition level
        SceneManager.LoadScene(4);
    }


}
