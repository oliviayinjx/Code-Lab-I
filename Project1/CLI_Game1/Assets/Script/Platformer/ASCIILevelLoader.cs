using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ASCIILevelLoader : MonoBehaviour
{
    //level txt file name
    public string fileName;
    //the x and y position of where our grid is starting (relative to the game object) 
    public float xOffset;
    public float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        //get the level txt and read to the end of txt
        StreamReader reader = new StreamReader(fileName);
        string contentOfFile = reader.ReadToEnd();
        reader.Close(); //close the file

        //check for the line break character
        char[] newLineChar = { '\n' };
        //Split based on \n, split into lines
        string[] level = contentOfFile.Split(newLineChar);
        //loop for each row in the level
        for(int i = 0; i < level.Length; i++)
        {
            MakeRow(level[i], -i); //create rows from bottom to top
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //takes 2 parameters, the characters of the row and the row's y pos
    void MakeRow(string rowStr, float y)
    {
        char[] rowArray = rowStr.ToCharArray(); //turn each row to an array of characters

        for (int x = 0; x < rowStr.Length; x++) // loop for the number of the characters in this row
        {
            char c = rowArray[x]; //store the current character as char c
            if (c == 'Y') //if character is Y
            {
                Debug.Log("yellow block");
                GameObject yellowBlock = Instantiate(Resources.Load("YellowBlock")) as GameObject; //create a yellow block from the resources folder
                yellowBlock.transform.position = new Vector3(
                    x * yellowBlock.transform.localScale.x + xOffset,
                    y * yellowBlock.transform.localScale.y + yOffset,
                    0
                    );
            }
            else if (c == 'B') //if the character is C
            {
                GameObject blueBlock = Instantiate(Resources.Load("BlueBlock")) as GameObject; //create a blue block from the resources folder
                //set the position of the new game object
                blueBlock.transform.position = new Vector3(
                    x * blueBlock.transform.localScale.x + xOffset,
                    y * blueBlock.transform.localScale.y + yOffset,
                    0
                    );
            }
        }
    }
}
