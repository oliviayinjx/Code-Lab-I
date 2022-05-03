using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    float dayTimer;
    public float dayStartTime = 0;

    [Header("Food")]
    [SerializeField]
    float foodInterval = 15f;
    bool giveFood = false;

    [SerializeField]
    private SpawnFood spawnFood;

    [Header("Water")]
    public GameObject drinker;
    float waterInterval = 20f;
    bool waterRefill = false;


    [Header("Restroom")]
    float cleanInterval = 60f;
    bool cleaned = false;

    // Start is called before the first frame update
    void Start()
    {
        dayTimer = dayStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        dayTimer += Time.deltaTime;
        //Debug.Log(dayTimer);

        if(Mathf.Round(dayTimer) % foodInterval == 0  && !giveFood)
        {
            Debug.Log("give food");
            giveFood = true;
            spawnFood.newFood();

        }
        else if (Mathf.Round(dayTimer) % foodInterval == 2)
        {
            giveFood = false;
        }


        if (Mathf.Round(dayTimer) % waterInterval == 0 && !waterRefill)
        {
            Debug.Log("give water");
            waterRefill = true;
            drinker.transform.localScale = new Vector3(0.1839385f, 1, 0.1839385f);


        }
        else if (Mathf.Round(dayTimer) % waterInterval == 2)
        {
            waterRefill = false;
        }

        if (Mathf.Round(dayTimer) % cleanInterval == 0 && !cleaned)
        {
            cleaned = true;

            GameObject[] destroyObject;
            destroyObject = GameObject.FindGameObjectsWithTag("Poop");
            foreach (GameObject oneObject in destroyObject)
                Destroy(oneObject);
        }
        else if (Mathf.Round(dayTimer) % cleanInterval == 2)
        {
            cleaned = false;
        }


    }
}
