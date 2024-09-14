using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackHitboxScript : MonoBehaviour
{
    public rockGuyScript rockguyRef;
    public basicMinionScript basicMinionScriptRef;
    public playerMovement playerMovementRef;
    public bool canDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        rockguyRef = GameObject.FindObjectOfType<rockGuyScript>();
        playerMovementRef = GameObject.FindObjectOfType<playerMovement>();
    }
    private void OnEnable()
    {
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
 
        if (other.gameObject.CompareTag("player")&&canDamage == true)
        {
            canDamage=false;
            playerMovementRef.playerTakeDamage(rockguyRef.damage);
            Rigidbody playerRb= other.gameObject.GetComponent<Rigidbody>();
            playerRb.AddForce(transform.up * 10, ForceMode.Impulse);
           
        }
        if (other.gameObject.CompareTag("minion")&&canDamage ==true)
        {
            canDamage=false;
            basicMinionScriptRef = other.GetComponent<basicMinionScript>();
            basicMinionScriptRef.takeDamage(rockguyRef.damage);
           
        }
    }
}
