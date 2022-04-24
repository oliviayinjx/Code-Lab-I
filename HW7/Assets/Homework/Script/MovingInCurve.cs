using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInCurve : MonoBehaviour
{
    private Vector3 startPos;
    //public float randomNum;
    private int randomSeed;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        randomSeed = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        switch(randomSeed){
            case 0:
                transform.position = startPos + new Vector3(Mathf.Sin(Time.time), 0f, 0f);
                break;

            case 1:
                transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time),0f);
                break;

            case 2:
                transform.position = startPos + new Vector3(Mathf.Sin(Time.time), Mathf.Cos(Time.time), 0f);
                break;
        }

        
    }


}
