using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float sphereRadius = 3f;
    public float zOffset = 10f;

    GameObject heldObj;
    Vector3 objOriginalPos;

    public GameObject drinker;
    float drinkTimer = 5f;
    bool drinkWater = false;

    [SerializeField]
    Transform reference; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cursorClick();
        if (drinkWater)
        {
            drinkTimer -= Time.deltaTime;
            if(drinkTimer <= 0)
            {
                drinkWater = false;
                drinkTimer = 5f; 
            }
        }
    }

    void cursorClick()
    {
        Vector3 eyePosition = transform.position;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //get the position between mouse and eye
        Vector3 dir = mouseWorldPos - eyePosition;
        dir.Normalize();

        RaycastHit hitter = new RaycastHit();
        Debug.DrawLine(eyePosition, dir * 5f, Color.red);

        if (Physics.SphereCast(eyePosition, sphereRadius, dir, out hitter))
        {
            Debug.DrawRay(eyePosition, dir, Color.red);
            //&& hitter.collider.gameObject.tag == "Food"

            //pickup object
            if(hitter.collider.gameObject.tag == "Food" && heldObj == null)
            {
                if (Input.GetMouseButton(0))
                {
                    //Debug.Break(); //check this later!!!!!!!!!!!!
                    Debug.Log("EAT");
                    PickupObject(hitter.collider.gameObject);
                    //Destroy(hitter.collider.gameObject);
                }

            }
             //Eat the food


            if (Input.GetMouseButton(1) && heldObj != null)
            {
                DropObject();
            }

            //drink water
            if (hitter.collider.gameObject.tag == "Drinker")
              {
                if (Input.GetMouseButton(0))
                {
                    if (!drinkWater)
                    {
                        drinkWater = true;
                        float ySize = drinker.transform.localScale.y;
                        ySize -= 0.2f;
                        drinker.transform.localScale = new Vector3(1, ySize, 1);
                    }
                }

              }
            
        }

        if (heldObj != null && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(heldObj);
        }

    }

    void PickupObject (GameObject obj)
    {
        heldObj = obj;
        objOriginalPos = obj.transform.position;
        obj.transform.SetParent(gameObject.transform);

        Vector3 newPos = reference.transform.position;
        obj.transform.position = newPos;
    }

    void DropObject()
    {
        heldObj.transform.SetParent(null);
        heldObj.transform.position = objOriginalPos;

        objOriginalPos = Vector3.zero;
        heldObj = null;
    }

    //private void OntriggerEnter(Collider other)
    //{
    //    Debug.Log("FIND");
    //    if (Input.GetKeyDown(KeyCode.F) && other.gameObject.tag == "Food")
    //    {
    //        Debug.Log("EAT");
    //        Destroy(other.gameObject);
    //    }
    //}
}
