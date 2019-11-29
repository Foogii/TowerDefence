using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public List<EnemyWave> enemyWaves = new List<EnemyWave>();

    private float elapsedTime = 0f;

    private EnemyWave activeWave;

    private float spawnCounter = 0f;

    private List<EnemyWave> activatedWaves = new List<EnemyWave>();

    void Start()
    {
        
    }


    void Awake()
    {
        Instance = this;
    }
    //2
    void Update()
    {
        elapsedTime += Time.deltaTime;
        SearchForWave();
        UpdateActiveWave();
    }
    private void SearchForWave()
    {
        //3
        foreach (EnemyWave enemyWave in enemyWaves)
        {
            //4
            if (!activatedWaves.Contains(enemyWave)
            && enemyWave.startSpawnTimeInSeconds <= elapsedTime)
            {

                //5
                activeWave = enemyWave;
                activatedWaves.Add(enemyWave);
                spawnCounter = 0f;
                //6
                break;
            }
        }
    }
    //7
    private void UpdateActiveWave()
    {
        if (activeWave != null)
        {
            spawnCounter += Time.deltaTime;
            //2
            if (spawnCounter >= activeWave.timeBetweenSpawnsInSeconds)
            {
                spawnCounter = 0f;
                //3
                if (activeWave.listOfEnemies.Count != 0)
                {
                    //4
                    GameObject enemy = (GameObject)Instantiate(
                    activeWave.listOfEnemies[0], WaypointManager.Instance.
                    GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);
                    //5
                    enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex;
                    //6
                    activeWave.listOfEnemies.RemoveAt(0);
                }
                else
                {
                    //7
                    activeWave = null;
                    //8
                    if (activatedWaves.Count == enemyWaves.Count)
                    {
                        // All waves are over
                    }
                }
            }
        }
    }

    public void StopSpawning()
    {
        elapsedTime = 0;
        spawnCounter = 0;
        activeWave = null;
        activatedWaves.Clear();
        enabled = false;
    }
}
