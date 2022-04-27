using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    public GameObject[] detectors;
    public bool wheelRotating = false;

    private float currentRot = 0;
    private float targetRot = 180;
    float rotationProgress = -1;
    public bool getRotating = false;
  
    public float getDir = 1;
    [SerializeField]
    float rotationSpeed = 0.2f;


    
    // Start is called before the first frame update
    void Start()
    {
        rotatingSetup();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(getRotating);
        if (getRotating)
        {
            startRotating();
            foreach (GameObject detector in detectors)
            {
                detector.GetComponent<BoxCollider>().enabled = false;
                detector.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else if (!getRotating)
        {
            //Debug.Log("zero");
            foreach (GameObject detector in detectors)
            {
                detector.GetComponent<BoxCollider>().enabled = true;
                detector.GetComponent<MeshRenderer>().enabled = true;
            }
        }


    }

    public void triggerRotate()
    {
        getRotating = true;
    }

    void rotatingSetup()
    {
        //Debug.Log("zero");
        rotationProgress = 0;
        getRotating = false;
    }

    void startRotating()
    {
        if (rotationProgress < 1 && rotationProgress >= 0)
        {
            rotationProgress += Time.deltaTime * rotationSpeed;
        }
        else if (rotationProgress >= 1)
        {
            currentRot = targetRot;

            getRotating = false;
            rotationProgress = 0;
        }

       gameObject.transform.rotation = Quaternion.AngleAxis(getDir * 180f * rotationProgress, Vector3.right);

        //float degreesPerSecond = 30;
        //wheel.transform.Rotate(transform.forward, Time.deltaTime * 180f );
        //wheel.transform.rotation = Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z * rotationProgress);

    }





}
