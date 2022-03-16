using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileIO : MonoBehaviour
{
    const string FILE_NAME = "DataSave.txt";
    private GameManager gameManager;
    private float highScore;
    void Start()
    {
        //find the game manager
        gameManager = GameManager.findInstance();
        //get the HighScore in gameManager and store in local highScore 
        highScore = gameManager.HighScore;

        //false == overwrite the file
        //true == add to the file
        StreamWriter writer = new StreamWriter(FILE_NAME, false);

        //write highscore in file
        writer.Write("High Score: " + highScore);
        writer.Close();

        StreamReader reader = new StreamReader(FILE_NAME);
        string content = reader.ReadLine();
        Debug.Log(content);

        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
