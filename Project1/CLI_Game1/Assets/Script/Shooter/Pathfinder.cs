using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //this script on enemy

    //reference to wave config object
    WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;
    //store the waypoints for the path
    List<Transform> waypoints;
    //starting at the firt waypoint and the list
    //keep track of which waypoint index we are on
    int waypointIndex = 0;
    private void Awake()
    {
        //find objects existing in the scene
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        //incase forgot to attach or attach wrong one
        //just get the waveConfig on enemy spawner
        waveConfig = enemySpawner.GetCurrentWave();
        //store all waypoints in waveconfig to waypoints list
        waypoints = waveConfig.GetWayPoints();
        //get the positions of all waypoints
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        //if this is not the last waypoint
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            //framework independent moving speed
            //get the moving speed in waveConfig script
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            //to move the enemy
            //target position is next point position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            //when move to the next position, add the index
            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        //get the end of the path
        else
        {
            //remove the ememy from the path 
            Destroy(gameObject);
        }
    }
}
