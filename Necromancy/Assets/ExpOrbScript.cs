using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbScript : MonoBehaviour
{
    public playerMovement playerRef;
    public GameObject player;
    public Rigidbody rb;
    public ExperiencePointScript experiencePointScriptRef;
    // Start is called before the first frame update
    void Start()
    {
        experiencePointScriptRef = GameObject.FindAnyObjectByType<ExperiencePointScript>();
        playerRef = GameObject.FindAnyObjectByType<playerMovement>();
        player = playerRef.gameObject;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.MoveTowards(transform.position, player.transform.position, 2 *Time.deltaTime);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.LookAt(player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position,5*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            playerRef.gainExperience(5);
            experiencePointScriptRef.upDateExprienceBar(playerRef.currentEXP, playerRef.maximumEXP);
            Destroy(gameObject);
        }
    }
}
