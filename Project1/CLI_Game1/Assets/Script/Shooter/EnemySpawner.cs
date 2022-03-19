using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO currentWave;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    //function to instantiate enermy according to the waveConfigSO 
    void SpawnEnemies()
    {
        for(int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            //when include position, also need rotation in quaternion
            //quaternion.identity: no rotation
            //transform: put inside the parent "enemy spawner"
            Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWayPoint().position,
                        Quaternion.identity, transform);
        }

    }

 
}
