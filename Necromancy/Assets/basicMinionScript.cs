using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class basicMinionScript : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody rb;
    public int targetIndex;
    public GameObject targets;
    public GameObject target;
    public GameObject[] enemyList;
    public GameObject player;
    public playerMovement playerBodyRef;
    public floatingHealthBar healthBar;
    public gameManagerScript gameManagerRef;
    public int speed;
    [Header("Debuging")]
    public bool targetPlayer;
    public bool targetEnemy;
    [Header("Stat")]
    public float targetDistance;
    public Vector3 targetRotation;
    public float rushCooldown;
    public bool canRush;
    public float rotationSpeed;
    public bool rushing;
    public bool grounded;
    public float currentSpeed;
    public float rushingTime;
    public float maxSpeed;
    public int health;
    public int maxHealth;
    public int damage;
    bool ramForward;
    public float distanceToPlayer;
    public float enemyDistance;
    public basicEnemyScript enemyRef;
    public Collider detectionCollider;



    // Start is called before the first frame update
    void Start()
    {
       
        gameManagerRef = GameObject.FindAnyObjectByType<gameManagerScript>();

        damage = 4;
        healthBar = gameObject.GetComponentInChildren<floatingHealthBar>();

        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        playerBodyRef = GameObject.FindAnyObjectByType<playerMovement>();
        targets = GameObject.Find("enemyHolder");
        maxHealth = gameManagerRef.minionMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.updateHealthBar(health,maxHealth);

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        
        if(target == player)
        {
            targetPlayer = true;
        }
        else
        {
            targetPlayer = false;
        }
        damage = gameManagerRef.minionMaxDmg;
        jump();
        coolDown();
        currentSpeed = rb.velocity.magnitude;
        if(currentSpeed > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if(grounded)
        {
            followTarget();
            chooseTarget();
            detectTarget();
        }

        
        rush();
        groundCheck();
        if(rushingTime > 0)
        {
            rushingTime -= Time.deltaTime;
        }
        if(canRush && !rushing)
        {
            rushing = true;
        }
       
    }

    public void detectTarget()
    {
      
        if (target != null)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            Vector3 direction =  new Vector3(directionToTarget.x, 0, directionToTarget.z);    
            targetRotation = directionToTarget.normalized;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.CompareTag("enemy") && rushCooldown <= 0)
                {
                    canRush = true;
                }
                else
                {
                    canRush = false;
                }
            }
        }
        else
        {
            target = playerBodyRef.gameObject;
        }
        
    }

    public void chooseTarget()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerBodyRef.transform.position);
        enemyList = GameObject.FindGameObjectsWithTag("enemy");


        if (enemyList.Length > 0)
        {
            for (targetIndex = 0; targetIndex < enemyList.Length; targetIndex++)
            {
                int i = targetIndex;
                float distance = Vector3.Distance(transform.position, enemyList[i].transform.position);
                if (distance < 4 || distanceToPlayer <4)
                {
                    rb.AddForce(-transform.forward * speed, ForceMode.Force);
                }
                if (distance < 12)
                {
                    target = enemyList[i].gameObject;
                    targetDistance = distance;
                    rb.AddForce(transform.forward * speed, ForceMode.Force);
                }
                



            }
        }
        else if(distanceToPlayer < 8)
                {
                    target = playerBodyRef.gameObject;
                    targetDistance = distanceToPlayer;
                    Debug.Log("moving to player");
                }
        else
        {
            target = playerBodyRef.gameObject;
            targetDistance = distanceToPlayer;
        }




    }

    void followTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            //transform.LookAt(target.transform);

            if (targetDistance > 8)
            {
                rb.AddForce(transform.forward * speed, ForceMode.Force);
                int randomNum = Random.Range(-10, 10);
                rb.AddForce(transform.right * randomNum, ForceMode.Force);
                Debug.Log("moving towards");
            }
            else if(targetDistance < 2) 
            {
                rb.AddForce(-transform.forward * (speed/2), ForceMode.Force);
                Debug.Log("moving away");
            }
        }


    }

    void rush()
    {
        
        if (rushing && canRush)
        {
            
            canRush = false;
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
            rushingTime = 2;
            rotationSpeed = 0.1f;
            //Invoke("rushForward", 0.5f);
            maxSpeed = 11;
        }

        else
        {
            rotationSpeed = 5f;
            rushing = false;
            maxSpeed = 5;
        }
    }
    void rushForward()
    {
        canRush = true;
    }

   void coolDown()
    {
        if(rushCooldown > 0)
        {
            rushCooldown -= Time.deltaTime;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy") && currentSpeed > 1)
        {
            basicEnemyScript enemyRef = collision.gameObject.GetComponent<basicEnemyScript>();
            ContactPoint contact = collision.contacts[0];
            Vector3 contactPostion = contact.point;
            Rigidbody enemyRb =  collision.gameObject.GetComponent<Rigidbody>();
            if(enemyRef.rammed != true)
            {
                rushCooldown = 5;
                enemyRb.velocity = Vector3.zero;
                enemyRb.AddForce(enemyRb.transform.forward * -25 , ForceMode.Impulse);
                enemyRb.AddForce(enemyRb.transform.up * 25, ForceMode.Impulse);
                enemyRef.rammed = true;
                enemyRef.takeDamage(damage);
                rushing = false ;
                Debug.Log("enemyRushed");
                
                
            }
            else
            {
                return;
            }

        }
    }
    

    void groundCheck()
    {
        BoxCollider col = gameObject.GetComponent<BoxCollider>();
        
        Ray ray = new Ray(transform.position, -transform.up * (col.size.y/2+ 0.2f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, col.size.y / 2 + 0.2f))
        {
            if (hit.collider != null)
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

    void jump()
    {
        BoxCollider col = gameObject.GetComponent<BoxCollider>();
        Ray ray = new Ray(transform.position, transform.forward * (col.size.x / 2 + 0.5f));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * (col.size.x / 2 + 0.5f), Color.green);
        if(Physics.Raycast(ray, out hit, col.size.y / 2 + 0.5f))
        {
            if (hit.collider.CompareTag("map")&&grounded)
            {
                rb.AddForce(transform.up * 10, ForceMode.Impulse);
            }
        }
        else
        {
            return;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("healBullet"))
        {
            health += 10;
            Debug.Log("Minions healed");
        }
        if(other.gameObject.CompareTag("enemy"))
        {
            target = other.gameObject;

        }
    }
  
}
