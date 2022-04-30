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
            drinker.transform.localScale = new Vector3(1, 1, 1);


        }
        else if (Mathf.Round(dayTimer) % waterInterval == 2)
        {
            waterRefill = false;
        }


    }
}
