using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJet : MonoBehaviour
{
    public float sphereRadius = 3f;
    public float zOffset = 10f;
    public GameObject waterObject;
    private ParticleSystem water;




    // Start is called before the first frame update
    void Start()
    {
        //find destructible script

    }

    // Update is called once per frame
    void Update()
    {
        //camera position
        Vector3 eyePosition = transform.position;
        //mouse position in screen
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = Camera.main.nearClipPlane;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //get vector direction
        Vector3 dir = mouseWorldPos - eyePosition;
        //unit vector
        dir.Normalize();

        RaycastHit hitter = new RaycastHit();
        //draw raycast line
        Debug.DrawLine(eyePosition, dir * 10f, Color.blue);

        if (Physics.SphereCast(eyePosition, sphereRadius, dir, out hitter))
        {
            //left mouse button, tag: breakable
            if (Input.GetMouseButton(0) && hitter.collider.gameObject.tag == "breakable"&& hitter.collider.gameObject!=null)
            {
                //get the vector between hitter and camera pos
                Vector3 dirN = (hitter.point - eyePosition).normalized;
                //spawn particle effect in this dir
                waterEffect(dirN);
                Debug.Log("break");
                //Destructible destructible = hitter.collider.GetComponent<Destructible>();
                //destructible.ToDestroy();
                //call ToDestory function in destructible objects
                hitter.collider.SendMessage("ToDestroy");
            }
        }
    }

    //for spawn new water particle effect when objects are hit
    void waterEffect(Vector3 direction)
    {
        //rotate according to the mouse position and dir vector
        Quaternion rotation = Quaternion.LookRotation(direction);
        //change rotation
        waterObject.transform.localRotation = rotation;
        water = waterObject.GetComponent<ParticleSystem>();
        //spawn a new particle
        ParticleSystem waterInstance = Instantiate(water);
        //destory after 1s
        Destroy(waterInstance, 1f);
    }

}
