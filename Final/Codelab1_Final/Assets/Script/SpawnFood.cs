using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject[] foods;

    private Vector3 Min;
    private Vector3 Max;

    private float _xAxis;
    private float _yAxis;
    private float _zAxis; //If you need this, use it
    private Vector3 _randomPosition;

    [SerializeField]
    private GameObject FoodParent;
    //spawn random food every 10 seconds

    // Start is called before the first frame update
    void Start()
    {
        SetRanges();
        _randomPosition = new Vector3(11.36f, -1.26f, 6.32f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            Application.Quit();

        _xAxis = Random.Range(Min.x, Max.x);
        _yAxis = Random.Range(Min.y, Max.y);
        _zAxis = Random.Range(Min.z, Max.z);
        _randomPosition = new Vector3(_xAxis, _yAxis, _zAxis);
    }

    public void newFood()
    {
        int randNum = Random.Range(0, foods.Length);
        GameObject spawned = Instantiate(foods[randNum], _randomPosition, Quaternion.identity);
        spawned.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
    }

    private void SetRanges()
    {
        Min = new Vector3(8.28f, 1.35f, 3.45f); //Random value.
        Max = new Vector3(13.5f, 1.35f, 9.41f); //Another ramdon value, just for the example.
    }
}
