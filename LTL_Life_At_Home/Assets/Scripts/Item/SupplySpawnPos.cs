using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplySpawnPos : MonoBehaviour
{
    public GameObject supplyPref;
    public float startSpawnTime, spawnTime, speed;
    bool isMoingRight;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = startSpawnTime;
        isMoingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameStage == GameStage.Playing)
        {
            CountDown();
        }

        if (isMoingRight)
        {
            transform.Translate(Vector2.right * speed);
        }
        else
        {
            transform.Translate(Vector2.left * speed);
        }

        if (transform.position.x >= 8.5f)
        {
            isMoingRight = false;
        }
        else if (transform.position.x <= -8.5f)
        {
            isMoingRight = true;
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
        Instantiate(supplyPref, transform.position, Quaternion.identity);
    }

    void MoveArround()
    {
        
    }
}
