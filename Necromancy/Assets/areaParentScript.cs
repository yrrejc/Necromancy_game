using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaParentScript : MonoBehaviour
{
    Transform[] children;
    public int childrenCount;
    public GameObject[] enemyTypes;
    public GameObject enemyChosen;
 
    // Start is called before the first frame update
    void Start()
    {
        children = transform.GetComponentsInChildren<Transform>();
        childrenCount = children.Length;
        for (int i = 0; i < children.Length; i++)
        {
            chooseEnemyType();
            children[i].gameObject.AddComponent<enemySpawn>();
            enemySpawn spawningArea = children[i].GetComponent<enemySpawn>();
            spawningArea.prefabToSpawn = enemyChosen.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void chooseEnemyType()
    {
        int randomNum = Random.Range(0, enemyTypes.Length);
        if(randomNum != 0)
        {
            enemyChosen = enemyTypes[randomNum];
        }
    }

    void enemySpawn()
    {

    }
}
