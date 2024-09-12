using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.Versioning;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class basicEnemyScript : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody rb;
    public int targetIndex;
    public GameObject target;
    public GameObject[] minionList;
    public playerMovement playerBodyRef;
    public floatingHealthBar healthBar;
    public GameObject expOrb;
    public int speed;
    public enemySpawn enemySpawnRef;
    [Header("Debuging")]
    public bool targetPlayer;
    public bool targetMinion;
    public bool walking;
    [Header("Stat")]
    public bool longRange;
    public float targetDistance;
    public Vector3 targetRotation;
    public bool grounded;
    public int health;
    public int maxHealth;
    public bool rammed;
    public float maxSpeed;
    public float currentSpeed;
    public gameManagerScript gameManagerRef;
    public float notGroundedTimer;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-90,0,0);
        enemySpawnRef = GameObject.FindAnyObjectByType<enemySpawn>();
        maxHealth = enemySpawnRef.enemyBaseHealth;
        notGroundedTimer = 0;
        rammed = false;
       
        health = maxHealth;
        rb = GetComponent<Rigidbody>();

        playerBodyRef = GameObject.FindAnyObjectByType<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        playerBodyRef = GameObject.FindAnyObjectByType<playerMovement>();
        if (target == null)
        {
            target = playerBodyRef.gameObject;
        }
        if (grounded != true)
        {
            notGroundedTimer += Time.deltaTime;
        }
        else
        {
            notGroundedTimer = 0;
        }
        if(notGroundedTimer > 10)
        {
            //Destroy(gameObject);
        }
        healthBar = gameObject.GetComponentInChildren<floatingHealthBar>();
        
        effects();
        groundCheck();
        if(!rammed || grounded ) 
        {
            followTarget();
        }
        //chooseTarget();
        detectTarget();
    }

    public void detectTarget()
    {
        Vector3 directionToTarget= (target.transform.position - transform.position).normalized;
        targetRotation = directionToTarget.normalized;
        Ray ray = new Ray(transform.position, directionToTarget);
        
        Debug.DrawRay(ray.origin, directionToTarget* 10, Color.red);
    }

    public void chooseTarget()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerBodyRef.transform.position);
        minionList = GameObject.FindGameObjectsWithTag("minion");
        
       
        if(minionList.Length > 0)
        {
            for (targetIndex = 0; targetIndex < minionList.Length; targetIndex++)
            {
                int i = targetIndex;
                float distance = Vector3.Distance(transform.position, minionList[i].transform.position);
                if (distance < 4 || distanceToPlayer < 4)
                {
                    target = minionList[i].gameObject;
                    targetDistance = distance;
                    rb.AddForce(-transform.forward * speed, ForceMode.Force);
                    
                }
                else if (distance > 5 || distanceToPlayer > 5)
                {
                    target = minionList[i].gameObject;
                    targetDistance = distance;
                    rb.AddForce(-transform.forward * speed, ForceMode.Force);
                   
                }
                else
                {
                    target = playerBodyRef.gameObject;
                    targetDistance = distanceToPlayer;
                    Debug.Log("moving to player");
                }



            }
        }
        else
        {
            target = playerBodyRef.gameObject;
            targetDistance = distanceToPlayer;
        }
        

    }

    //more like look at target
    void followTarget()
    {
        Vector3 clampSpeed = rb.velocity.normalized * maxSpeed;
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        directionToTarget.y = 0;
        targetRotation = directionToTarget.normalized;

        transform.rotation = Quaternion.LookRotation(targetRotation);
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if(distanceToTarget > 4)
        {
            walking = true;
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }
        
        else if (distanceToTarget < 3)
        {
            walking = true;
            rb.AddForce(-transform.forward * speed, ForceMode.Force);

        }

        else if (currentSpeed >= maxSpeed)
        {
           walking = true;
            rb.velocity = clampSpeed;
        }
        else
        {
           walking = false;    
        }
        
       

    }
    void groundCheck()
    {
        BoxCollider col = gameObject.GetComponent<BoxCollider>();
        if(col!= null)
        {
            Ray ray = new Ray(transform.position, -transform.up * (col.size.y / 2 + 0.2f));
            Debug.DrawRay(ray.origin, ray.direction * (col.size.y / 2 + 0.2f), Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, col.size.y / 2 + 0.2f))
            {
                if (hit.collider.CompareTag("ground"))
                {
                    grounded = true;
                }
                else
                {
                    grounded = false;
                }
            }
            else
            {
                grounded = false;
            }
        }
       
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        healthBar.updateHealthBar(health, maxHealth);
        Debug.Log("enemy took damage");
        if (health <= 0)
        {
            Instantiate(expOrb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }        
    }
    public void effects()
    {
        if (rammed)
        {
            Invoke("unRam", 0.5f);
           
        }
        else
        {
            
        }
    }

    public void unRam()
    {
        rammed = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            target = other.gameObject;
        }
        if (other.gameObject.CompareTag("minion"))
        {
            target = other.gameObject;
        }
      
    }
}


