using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeManagerScript : MonoBehaviour
{

    public GameObject[] slotPositions;
    public GameObject[] upgradebuttons;
    public GameObject upgrade;
    public int randomUpgradeIndex;
    public int position;
    public List<int> availSlots;
    public playerMovement playerRef;
    public basicMinionScript minionRef;
    public bool on;
    public UImanagerScript UiRef;

    public void OnEnable()
    {
        on = true;
        showUpgrade();
        Time.timeScale = 0.01f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {

        on = false;
        Time.timeScale = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        UiRef = GameObject.FindAnyObjectByType<UImanagerScript>();
        playerRef = GameObject.FindAnyObjectByType<playerMovement>();
        minionRef = GameObject.FindAnyObjectByType<basicMinionScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(on == true){
            Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
       
    }

    public void showUpgrade()
    {
        for (int i = 0; i < upgradebuttons.Length; i++)
        {
            availSlots.Add(i);
        }
        for (int i = 0; i < slotPositions.Length; i++)
        {
            randomUpgradeIndex = Random.Range(0, availSlots.Count);
            int randomUpgrade = availSlots[randomUpgradeIndex];
            upgrade = Instantiate(upgradebuttons[randomUpgrade], slotPositions[i].transform.position, Quaternion.identity);
            upgrade.transform.SetParent(transform, true);
            availSlots.RemoveAt(randomUpgradeIndex);
            
        }
    }


    public void moreHealth()
    {
        minionRef.maxHealth += 10;
    }
    public void moreDamage()
    {
        minionRef.damage += 4;
    }
    public void moreMinions()
    {
        playerRef.maxMinion += 1;
    }


}
