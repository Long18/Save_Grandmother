using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPref;

    public float startSpawnTime, spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = startSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameStage == GameStage.Playing)
        {
            CountDown();
        }
    }

    void CountDown()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            spawnTime = startSpawnTime;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPref, transform.position, Quaternion.identity);
    }
}
