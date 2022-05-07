using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float sphereRadius = 1f;
    public float zOffset = 10f;

    GameObject heldObj;
    Vector3 objOriginalPos;

    [Header("Drinker")]
    public GameObject drinker;
    float drinkTimer = 5f;
    bool drinkWater = false;

    [Header("Reference")]
    [SerializeField]
    Transform pickupRef;
    [SerializeField]
    Transform dropoffRef;
    [SerializeField]
    float dropOffHeight = 1.35f;

    [Header("Restroom")]
    [SerializeField]
    GameObject poopMesh;
    public GameObject restroom;
    public GameObject pops;
    float poopTimer = 5f;
    bool pooped = false;


    private GameObject _mainCamera;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }


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

        if(pooped)
        {
            poopTimer -= Time.deltaTime;
            if (poopTimer <= 0)
            {
                pooped = false;
                poopTimer = 2f;
            }
        }
    }

    void cursorClick()
    {
        Vector3 eyePosition = _mainCamera.transform.position;
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
                        float xzSize = drinker.transform.localScale.x;
                        float ySize = drinker.transform.localScale.y;
                        ySize -= 0.2f;
                        drinker.transform.localScale = new Vector3(xzSize, ySize, xzSize);
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

        Vector3 newPos = pickupRef.transform.position;
        obj.transform.position = newPos;
    }

    void DropObject()
    {
        heldObj.transform.SetParent(null);
        heldObj.transform.position = new Vector3 (dropoffRef.transform.position.x, dropoffRef.transform.position.y, dropoffRef.transform.position.z);

        objOriginalPos = Vector3.zero;
        heldObj = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Restroom" && !pooped)
        {
            pooped = true;

            int rand = Random.Range(0, 2);

            switch (rand)
            {
                case 0:
                    Instantiate(poopMesh, dropoffRef.transform.position, Quaternion.identity);
                    Debug.Log("poop");
                    break;
                case 1:

                    break;
            }



        }
    }
}
