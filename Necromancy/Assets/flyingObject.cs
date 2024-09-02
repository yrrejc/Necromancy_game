using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class flyingObject : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    public int maxFlyingHeight;
    public int minFlyingHeight;
    public int flyingStrength;
    public GameObject modelBase;
    public bool flying;
    public float flyingSpeed;
    public float forwardSpeed;
    public float maxFloatSpeed;
    public float speed;
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
        Vector3 velocity = rb.velocity;
        forwardSpeed = velocity.x;
        flyingSpeed = velocity.y;
        speed = rb.velocity.magnitude;
        
        Vector3 clampYSpeed = rb.velocity.normalized * maxFloatSpeed;
        Vector3 clampSpeed = rb.velocity.normalized * maxSpeed;
        if (flyingSpeed > maxFloatSpeed)
        {
            flyingSpeed = clampYSpeed.y;
            rb.velocity = new Vector3(rb.velocity.x, flyingSpeed , rb.velocity.z);

        }
        if (speed > maxSpeed)
        {
            forwardSpeed = clampSpeed.x;
            rb.velocity = clampSpeed;
        }
        //rb.velocity = velocity;

        castRay();
        if (!flying)
        {
            fly();
        }

    }

    void castRay()
    {
        Ray ray = new Ray(transform.position, -transform.up * minFlyingHeight);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * minFlyingHeight, Color.yellow);
        float randomSway = Random.Range(minFlyingHeight - (minFlyingHeight/3), minFlyingHeight + (minFlyingHeight / 3));
        if (Physics.Raycast(ray, out hit, randomSway))
        {
            if (hit.collider.CompareTag("ground"))
            {
                flying = false;
                
                
            }
            
        }
        if (hit.collider == null)
        {
            flying = true;

        }

    }

    void fly()
    {
        int randomStrength = Random.Range(flyingStrength - 1, flyingStrength + 1);
        rb.AddForce(transform.up * flyingStrength, ForceMode.Force);
    }
    void moveForward()
    {
        
        rb.AddForce(transform.forward * 5, ForceMode.Force);
        
    }
}
