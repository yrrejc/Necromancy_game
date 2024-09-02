using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectileScript : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("minion"))
        {
            basicMinionScript minionRef = other.GetComponent<basicMinionScript>();
            minionRef.takeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("player"))
        {
            playerMovement playerRef = other.GetComponent<playerMovement>();
            playerRef.playerTakeDamage(damage);
            Debug.Log("hit player");
            Destroy(gameObject);    
        }
    }
}
