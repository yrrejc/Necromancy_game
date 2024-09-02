using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class enemySpawn : MonoBehaviour
{
    Bounds bounds;
    float randomX;
    float randomY;
    float randomZ;
    public GameObject CUBE;
    public GameObject prefabToSpawn;
    public GameObject spawnArea;
    public int enemyCount;
    public bool canSpawnEnemy;
    public basicEnemyScript enemyRef;
    public basicEnemyScript[] enemyList;
    public int enemySpawnLimit;
    public int enemyWaveCount;
    public GameObject enemyWaveCountTxt;
    public int enemyBaseHealth;



    // Start is called before the first frame update
    void Start()
    {
        if (enemyRef != null)
        {
            enemyBaseHealth = enemyRef.maxHealth;
        }
        enemyWaveCount = -1;
        enemySpawnLimit = 3;
        spawn();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawnEnemy)
        {
            spawn();
        }
        //counting all enemies from the game
        enemyList = FindObjectsOfType<basicEnemyScript>();
        enemyCount = enemyList.Length;
        //allowing enemies to spawn after the enemy count reach 0
        if (enemyCount == 0)
        {
            canSpawnEnemy = true;
        }
        else
        {
            canSpawnEnemy = false;
        }
        enemyWaveCountTxt = GameObject.Find("WaveCount");
        Text waveText =enemyWaveCountTxt.GetComponent<Text>();
        waveText.text="Wave: "+enemyWaveCount;


    }

    void spawn()
    {
        for (int i = 0; i < enemySpawnLimit; i++)
        {
            if (enemyCount < enemySpawnLimit)
            {
                Vector3 randomSpawn = randomSpawnLocation();
                Instantiate(prefabToSpawn, randomSpawn, Quaternion.identity);
            }
          

        }
        enemySpawnLimit += 2;
        enemyWaveCount += 1;
        int randomNum = Random.Range(5, 10);
        enemyBaseHealth += randomNum;


    }
    Vector3 randomSpawnLocation()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            bounds = gameObject.GetComponent<MeshRenderer>().bounds;
        }
        randomX = Random.Range(bounds.min.x, bounds.max.x);
        randomY = Random.Range(bounds.min.y, bounds.max.y);
        randomZ = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(randomX, randomY + 1f, randomZ);

    }
}
