using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    [SerializeField]
    GameObject wheel;
    [SerializeField]
    float direction = 1;

    WheelRotate wheelRotate;

    //private bool rotateDone; 

    // Start is called before the first frame update
    void Start()
    {
        wheelRotate = wheel.GetComponent<WheelRotate>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
       if (other.name == "Player")
        {
            //rotating = true;
            //Debug.Log("yes");
            wheelRotate.getDir = direction;
            wheelRotate.SendMessage("triggerRotate");
            //wheelRotate.getRotating = true;

        }

    }
}
