using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UImanagerScript : MonoBehaviour
{
    [Header("UI state")]
    public float coolDown;
    public float maxCoolDown;
    public bool canSpawn;
    public bool usingUi;
    [Header("Input")]
    public bool UIkey;
    [Header("Component")]
    public GameObject UICanvas;
    public GameObject Player;
    public playerMovement PlayerRef;
    [Header("Spawnables")]
    public GameObject Minion1;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        coolDown = maxCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindAnyObjectByType<playerMovement>().gameObject;
        PlayerRef = GameObject.FindAnyObjectByType<playerMovement>();

        input();
        UI();
        if(!canSpawn){
            resetCoolDown();
        }
        if(coolDown == maxCoolDown){
            canSpawn = true;
        }
    }

    void input()
    {
        UIkey = Input.GetKey(KeyCode.Tab);
    }
    private void UI()
    {
        if (UIkey)
        {
            usingUi = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
          
            UICanvas.SetActive(true);
        }
        else
        {
            usingUi = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UICanvas.SetActive(false);
        }
    }

    public void spawnMinion1()
    {
        if (PlayerRef != null&& canSpawn == true)
        {
            canSpawn = false;
            if (PlayerRef.currentMinion < PlayerRef.maxMinion)
            {
                GameObject Minion = Instantiate(Minion1, Player.transform.position, Quaternion.identity);
                Minion.tag = "minion";
            }
        }
        
       
    }

    void resetCoolDown()
    {
        if(coolDown >= 0){
            coolDown -= Time.deltaTime;

        }
        if(coolDown <= 0){
            coolDown = maxCoolDown;
            canSpawn = true;
        }    
    }
}
