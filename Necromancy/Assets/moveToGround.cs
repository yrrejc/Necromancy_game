using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToGround : MonoBehaviour
{
    public Vector3 ground;
    // Start is called before the first frame update
    void Start()
    {
        float randomSize = Random.Range(1f, 1.5f);
        transform.localScale = new Vector3(transform.localScale.x * randomSize, transform.localScale.y * randomSize, transform.localScale.z * randomSize);
        goDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void goDown()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            ground = hit.point;
        }

        transform.position = ground + transform.up*1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("placeable"))
        {
            int randomDestroy = Random.Range(0, 10);
            if (randomDestroy > 5)
            {
                Destroy(gameObject);
                Debug.Log("destroyed self");
            }
            if (randomDestroy < 5)
            {
                Destroy(collision.gameObject);
                Debug.Log("destroyed other");
            }

        }
        if (collision.collider.CompareTag("door"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("door"))
        {
            Destroy(gameObject);
        }
    }
}
