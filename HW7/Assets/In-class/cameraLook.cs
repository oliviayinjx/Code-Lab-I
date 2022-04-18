using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //starting point
        Vector3 eyePosition = transform.position;
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //get rid of the direction
        Vector3 dir = mouseWorldPos - eyePosition;
        dir.Normalize(); //turn to value 1 or -1, just the way that going

        RaycastHit hitter = new RaycastHit();

        //out hitter: return the thing we hit
        if (Physics.Raycast(eyePosition,dir, out hitter))
        {
            Debug.Log("hit something");
            Debug.Log(hitter.collider.gameObject.name);
        }
    }

}
