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
    }

    // Update is called once per frame
    void Update()
    {
        _xAxis = Random.Range(Min.x, Max.x);
        _yAxis = Random.Range(Min.y, Max.y);
        _zAxis = Random.Range(Min.z, Max.z);
        _randomPosition = new Vector3(_xAxis, _yAxis, _zAxis);
    }

    public void newFood()
    {
        int randNum = Random.Range(0, foods.Length);
        Instantiate(foods[randNum], _randomPosition, Quaternion.identity);
    }

    private void SetRanges()
    {
        Min = new Vector3(5, 1.35f, 2); //Random value.
        Max = new Vector3(13, 1.35f, 13); //Another ramdon value, just for the example.
    }
}
