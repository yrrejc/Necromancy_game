using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour
{
    public int sceneNumber;
    public GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerRef = GameObject.FindAnyObjectByType<playerMovement>().gameObject;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (playerRef == null && sceneIndex == 1 )
        {
            SceneManager.LoadScene(0);
        }
    }

    public void loadScene()
    {
        SceneManager.LoadScene(1);

    }

    
}
