using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelGeneratorScript : MonoBehaviour
{
    public int roomLevel;
    public bool objectSpawned;
    public int randomRoomNumber;
    public bool newRoom;
    [Header("RandomSpawnLoaction")]
    public float randomX;
    public float randomY;
    public float randomZ;
    [Header("Componenets")]
    public GameObject Player;
    public GameObject PlayerRef;
    public GameObject spawnArea;
    public GameObject[] rooms;
    public GameObject[] objects;
    public GameObject roomSpawned;
    public GameObject door;
    Bounds spawnableArea;
    public LevelScript RoomRef;
    public GameObject spawnedDoor;
    public bool doorSpawned;
    public bool playerTeleported;
    public GameObject[] enemies;
    public GameObject[] enemyInGame;
    public int enemyAlive;
    public GameObject[] minionAlive;
    // Start is called before the first frame update
    void Start()
    {
       doorSpawned = false;
        Instantiate(Player, transform.position,Quaternion.identity);
        newRoom = false;
        objectSpawned = false;
        roomGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        minionAlive = GameObject.FindGameObjectsWithTag("minion");
        enemyInGame = GameObject.FindGameObjectsWithTag("enemy");
        enemyAlive = enemyInGame.Length;
        PlayerRef = GameObject.FindAnyObjectByType<playerMovement>().gameObject;
        if (GameObject.FindGameObjectWithTag("door")!= null)
        {
            doorSpawned = true;
        }
        else
        {
            doorSpawned = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            makeNewRoom();
        }
   


    }
    void randomSpawn()
    {

        Vector3 randomPosition = randomSpawnLocation();
        int randomObjNum = Random.Range(3, 7);
        int enemyNum = 5;
        for (int i = 0; i < randomObjNum; i++)
        {
            int randomObject = Random.Range(0, objects.Length);
            int randomRotationY = Random.Range(0, 360);
            Vector3 randomPostition = randomSpawnLocation();
            //roomSpawned = Instantiate(objects[randomObject], randomPostition, Quaternion.Euler(0, randomRotationY, 0), transform);

        }
        enemyNum = enemyNum += roomLevel*3;
        for (int i = 0; i < enemyNum; i++)
        {
            int randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomEnemy], randomPosition, Quaternion.identity);
        }
        objectSpawned = true;
    }

    Vector3 randomSpawnLocation()
    {
        if (spawnArea != null)
        {
            spawnableArea = spawnArea.GetComponent<MeshRenderer>().bounds;
        }
        randomX = Random.Range(spawnableArea.min.x, spawnableArea.max.x);
        randomY = Random.Range(spawnableArea.min.y, spawnableArea.max.y);
        randomZ = Random.Range(spawnableArea.min.z, spawnableArea.max.z);
        return new Vector3 (randomX, randomY + 1f, randomZ);

    }
    public void roomGeneration()
    {
        Invoke("spawnDoor", 0.1f);
        playerTeleported = false;
        randomRoomNumber = Random.Range(0, rooms.Length);
        GameObject roomSpawned =  Instantiate(rooms[randomRoomNumber],transform.position,Quaternion.identity,transform);
        RoomRef = roomSpawned.GetComponent<LevelScript>();
        spawnArea = roomSpawned.GetComponentInChildren<spawnAreaScript>().gameObject;
        if (objectSpawned == false)
        {
            randomSpawn();
        }
        
    }

    void destoryRoom()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);  
            objectSpawned = false;
        }
        
    }
    void spawnDoor()
    {
        Vector3 randomPosition = randomSpawnLocation();
        spawnedDoor = Instantiate(door, randomPosition , Quaternion.identity, transform);
        
        
    }
    void teleportPlayer()
    {
        PlayerRef.transform.position = spawnedDoor.transform.position + spawnedDoor.transform.forward * 1;
    }
    public void makeNewRoom()
    {
        roomLevel++;
        camController camRef = GameObject.FindAnyObjectByType<camController>();
        Invoke("destoryRoom", 0.5f);
        Invoke("roomGeneration", 0.55f);
        Invoke("disableCam", 0.49f);
        Invoke("ReEnableCam", 1.5f);
        Invoke("teleportMinion", 1f);
        Invoke("teleportPlayer", 0.8f);

    }
    void disableCam()
    {
        camController camRef = GameObject.FindAnyObjectByType<camController>();
        camRef.cam.enabled = false;
    }
    private void ReEnableCam()
    {
        camController camRef = GameObject.FindAnyObjectByType<camController>();
        camRef.reEnableCam();
    }
    void teleportMinion()
    {
        for (int i = 0; i < minionAlive.Length; i++)
        {
            minionAlive[i].transform.position = PlayerRef.transform.position;
        }
    }


}
