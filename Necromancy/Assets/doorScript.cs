using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public Vector3 ground;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Physics.Raycast(ray, out hit))
        {
            ground = hit.point;
        }

        transform.position = ground + transform.up *1;
    }
}
