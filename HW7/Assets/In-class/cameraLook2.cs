using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLook2 : MonoBehaviour
{
    public float sphereRadius = 3f;
    public float zOffset = 10f;

    GameObject heldObj;
    Vector3 objOriginalPos;
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
        Debug.DrawLine(eyePosition,dir * 10f,Color.red);

        //out hitter: return the thing we hit
        //if (Physics.Raycast(eyePosition, dir, out hitter))
        //{
        //    Debug.Log("hit something");
        //    Debug.Log(hitter.collider.gameObject.name);
        //}

        if (Physics.SphereCast(eyePosition, sphereRadius, dir, out hitter))
        {
            Debug.DrawRay(eyePosition, dir, Color.red);
            //left button
            if(Input.GetMouseButton(0) && hitter.collider.gameObject.tag == "pickable" && heldObj == null)
            {
                Debug.Break();
                Debug.Log("can pickup");
                PickupObject(hitter.collider.gameObject);
            }
        }

        //right button
        if(Input.GetMouseButton(1) && heldObj != null)
        {
            DropObject();
        }
    }

    void PickupObject(GameObject obj)
    {
        heldObj = obj;
        objOriginalPos = obj.transform.position;
        obj.transform.SetParent(gameObject.transform);

        Vector3 newPos = new Vector3(transform.position.x,transform.position.y,transform.position.z+ zOffset);

        obj.transform.position = newPos; 
    }

    void DropObject()
    {
        heldObj.transform.SetParent(null);
        heldObj.transform.position = objOriginalPos;

        objOriginalPos = Vector3.zero;
        heldObj = null; 
    }
}
