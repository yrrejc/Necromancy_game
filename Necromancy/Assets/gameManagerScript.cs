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
    public int attackToken;
    public int maxAttackToken;
    // Start is called before the first frame update
    void Start()
    {
        minionMaxDmg = 4;
        attackToken = maxAttackToken;
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
        reduceToken();
    }


    void spawnEnemy()
    {
        
    }

    void reduceToken()
    {
        for (int i = attackToken; i > 0; i++)
        {
            StartCoroutine("reducingToken");
        }
    }

    IEnumerator reducingTokens()
    {
        attackToken -= 1;
        yield return new WaitForSeconds(1);
    }
}
