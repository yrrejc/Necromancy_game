using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    public int enemyCount;
    public bool canSpawnEnemy;
    public basicEnemyScript enemyRef;
    public basicEnemyScript[] enemyList;
    public GameObject[] enemyTypes;
    public int minionMaxDmg;
    public int minionMaxHealth;
    public basicMinionScript[] minionList;
    public upgradeStatScript upgradeStatScriptRef;
    // Start is called before the first frame update
    void Start()
    {
        minionMaxDmg = 4;
        minionMaxHealth = 10;

    }

    // Update is called once per frame
    void Update()
    {
        minionList = FindObjectsOfType<basicMinionScript>();
        upgradeStatScriptRef = FindObjectOfType<upgradeStatScript>();
        for (int i = 0; i < minionList.Length; i++)
        {
            minionList[i].damage = minionMaxDmg;


        }
        for (int i = 0; i < minionList.Length; i++)
        {
            minionList[i].maxHealth = minionMaxHealth;
        }
    }


    void spawnEnemy()
    {
        
    }
}
