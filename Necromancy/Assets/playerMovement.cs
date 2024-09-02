using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class playerMovement : MonoBehaviour
{
    [Header("stats")]
    public int level;
    public float maximumEXP;
    public float currentEXP;
    public int maxMinion;
    public int currentMinion;
    public int maxHealth;
    public int currentHealth;
    public bool dead;
    [Header("Movement stats")]
    public float speed;
    public float speedSetting;
    public float sprintSpeed;
    public float jumpHeight;
    public float ySpeed;
    public float currentSpeed;
    public float maxSpeed;
    public bool grounded;
    public bool falling;
    public bool jumping;
    public Vector3 inAirSpeed;
    [Header("Inputs")]
    public bool forward;
    public bool backward;
    public bool left;
    public bool right;
    public bool jumpKey;
    public bool sprint;
    public bool fireKey;
    [Header("Component")]
    private Rigidbody rb;
    public Transform orientation;
    public GameObject[] minionList;
    public GameObject upgradeRef;
    public floatingHealthBar healthBar;
    public Text levelDisplay;
    public GameObject playerModel;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        healthBar = GetComponentInChildren<floatingHealthBar>();
        maxHealth = 100;
        currentHealth = maxHealth;
        //disabled for now as top down doesnt need it currently
        //upgradeRef = GameObject.FindAnyObjectByType<upgradeManagerScript>().gameObject;
        if( upgradeRef != null)
        {
            upgradeRef.SetActive(false);

        }
        maximumEXP = 10;
        maxMinion = 2;
        
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
        healthBar.updateHealthBar(currentHealth, maxHealth);
        minionList = GameObject.FindGameObjectsWithTag("minion");
        if( minionList != null )
        {
            currentMinion = minionList.Length;

        }
        //all methods that are required to be check all the time
        myInput();
        move();
        groundCheck();
        jump();
        fallingCheck();
        jumpingCheck();
        //changing the orientation to where the camera is facing
        transform.rotation = orientation.rotation;
        //changing speed and drag while in air and to compensate for the lack of frictions
        if(falling && !grounded)
        {
            rb.AddForce(-transform.up * 20* Time.deltaTime * 100, ForceMode.Force);
        }
        if (grounded != true)
        {
            
            inAirSpeed = rb.velocity;
            inAirSpeed.y = rb.velocity.y * 1;
            rb.velocity = inAirSpeed;
            speed = speedSetting / 4;
            rb.drag = 0;
        }
        else
        {
            speed = speedSetting;
            rb.drag = 6;

        }
    }

    private void move()
    {
      
        currentSpeed = rb.velocity.magnitude;
        Vector3 clampSpeed = rb.velocity.normalized * maxSpeed;
        if(sprint && grounded)
        {
            speed = sprintSpeed;
        }
        if(currentSpeed > maxSpeed)
        {
            rb.velocity = clampSpeed;
        }
        if(forward)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }
        if (backward)
        {
            rb.AddForce(-transform.forward * speed, ForceMode.Force);
        }
        if(left)
        {
            rb.AddForce(-transform.right * speed, ForceMode.Force);
        }
        if(right)
        {
            rb.AddForce(transform.right * speed, ForceMode.Force);
        }
    }
    private void groundCheck()
    {
        float rayDistance = (GetComponent<CapsuleCollider>().height/2) + 0.2f;
        Ray groundCheckRay = new Ray(transform.position, -transform.up * rayDistance);
        Debug.DrawRay(groundCheckRay.origin, groundCheckRay.direction * rayDistance, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(groundCheckRay, out hit, rayDistance) && !hit.collider.CompareTag("spawnArea"))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

    }
    private void jump()
    {
     
      if(grounded == true && jumpKey == true)
        {
            Vector3 brakeImpulse = -rb.velocity * 0.5f;
            rb.AddForce(brakeImpulse, ForceMode.VelocityChange);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }
    private void myInput()
    {
        sprint = Input.GetKey(KeyCode.LeftShift);
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        jumpKey = Input.GetKeyDown(KeyCode.Space);
    }
    void fallingCheck()
    {
        ySpeed = rb.velocity.y;
        if(ySpeed < 0)
        {
            falling = true; 
        }
        else
        {
            falling = false;
        }
    }
    void jumpingCheck()
    {
        if(!falling && !grounded)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
    }




    public void gainExperience(float amount)
    {
        currentEXP += amount;
        if (currentEXP >= maximumEXP)
        {
            level++;
            currentHealth = maxHealth;
            upgradeRef.SetActive(true);
                
            if (upgradeRef != null)
            {
                upgradeRef.gameObject.SetActive(true);
            }
            maximumEXP += 10;
            currentEXP = 0;
        }
    }

    public void playerTakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            dead = true;
            Debug.Log("player Died");
            GetComponent<MeshRenderer>().enabled = false;
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }
    }
  
}
