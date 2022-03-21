using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs; //multiple waves
    [SerializeField] float timeBetweenWaves = 0f; //wait time between waves
    [SerializeField] bool isLooping; 
    WaveConfigSO currentWave;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemiesWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    //function to instantiate enermy according to the waveConfigSO
    //IEnumerator: return sth for unity to track of
    IEnumerator SpawnEnemiesWaves()
    {
        //infinite spwawn enemy 
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    //when include position, also need rotation in quaternion
                    //quaternion.identity: no rotation
                    //transform: put inside the parent "enemy spawner"
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.GetStartingWayPoint().position,
                                Quaternion.identity, transform);
                    //spawn next wave after random time in waveconfigso code.
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping == true);
        //spawn multiple waves with multiple enemies



    }

 
}
