using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerLevelScript : MonoBehaviour
{

    playerMovement playerRef;
    public Text levelText;
    public int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        playerRef = GameObject.FindAnyObjectByType<playerMovement>();
        currentLevel = playerRef.level;
        levelText.text = currentLevel.ToString();
    }
}
