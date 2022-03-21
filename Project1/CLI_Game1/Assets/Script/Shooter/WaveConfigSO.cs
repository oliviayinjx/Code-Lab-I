using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ata container that you can use to save large amounts of data
[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject //change monobehavior to scriptable object
{
    //store all enemies that going to be instantiating
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    //spawn at different time
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f; //give randomness to spawn pace
    [SerializeField] float minimumSpawnTime = 0.2f;

    int enemyCount;

    //get the first starting point on the wave
    public Transform GetStartingWayPoint()
    {
        return pathPrefab.GetChild(0);
    }

    //get all points on the wave
    public List<Transform> GetWayPoints()
    {
        List<Transform> waypoints = new List<Transform>();
        //use foreach loop to get all childs in the pathprefab list
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    //return the moving speed of enemy to other script
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    //return the number of enemies in the enemyPrefabs list
    public int GetEnemyCount()
    {
        enemyCount = enemyPrefabs.Count;
        return enemyCount;
    }

    //return the enemy prefab at the given index
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        //give a random spawn value in the range
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
                                    timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
