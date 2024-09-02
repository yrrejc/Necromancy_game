using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeStatScript : MonoBehaviour
{
    // Start is called before the first frame update

    public playerMovement playerRef;
    public basicMinionScript minionRef;
    public basicMinionScript[] minionList;
    public GameObject upgradeCanvas;
    public Button button;
    public upgradeManagerScript upgradeRef;
    public gameManagerScript gameManagerRef;
    public int upgradedDmg;
    public int baseDmg;

    // Start is called before the first frame update
    void Start()
    {
        baseDmg = 4;
        playerRef = GameObject.FindAnyObjectByType<playerMovement>();
        gameManagerRef = GameObject.FindAnyObjectByType<gameManagerScript>();
        minionRef = GameObject.FindAnyObjectByType<basicMinionScript>();
        minionList = FindObjectsOfType<basicMinionScript>();
        upgradeCanvas = GameObject.FindAnyObjectByType<upgradeManagerScript>().gameObject;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        gameManagerRef.minionMaxDmg = baseDmg;
    }
    void OnButtonClick()
    {
        upgradeRef = GameObject.FindAnyObjectByType<upgradeManagerScript>();
        upgradeRef.gameObject.SetActive (false);
        Destroy(gameObject);
    }




    public void moreHealth()
    {
        
        gameManagerRef.minionMaxHealth += 10;
    }
    public void moreDamage()
    {
       
        gameManagerRef.minionMaxDmg +=10;
    }
    public void moreMinions()
    {
        playerRef.maxMinion += 1;
    }

    public void upgraded()
    {
        upgradeCanvas.SetActive(false);
    }

}
