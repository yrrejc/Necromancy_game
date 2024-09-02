using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public Text levelText;
    public playerMovement playerPrefs;
    // Start is called before the first frame update
    void Start()
    {
        levelText = GetComponent<Text>();
        playerPrefs = GameObject.FindAnyObjectByType<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = playerPrefs.level.ToString();
    }   

    public void selfDestroy()
    {

    }
}
