using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topDownCamera : MonoBehaviour
{
    public GameObject orientation;
    public float distance;
    float scrollInput;
    Camera thisCamera;
    public GameObject CUBE;
    public GameObject playerModel;
    public GameObject playerRef;
    public GameObject camPostition;
    Rigidbody playerRb; 
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();

        //transform.rotation = quaternion.Euler(90,transform.rotation.y,transform.rotation.z);
        playerRb = playerRef.GetComponent<Rigidbody>();

   
    }
    private void OnEnable()
    {
        
        playerRb.angularVelocity = Vector3.zero;
        playerRb.angularDrag = 1000;

    }

    // Update is called once per frame
    void Update()
    {
        playerRb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        //transform.position = camPostition.transform.position;
        faceCursor();
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if(scrollInput != 0)
        {
            Debug.Log(scrollInput);
        }
       float targetDistance = distance + scrollInput * -1000;
       float currentDistance = Mathf.Lerp(distance, targetDistance, Time.deltaTime);
       distance = currentDistance;
        if(distance < 2)
        {
            distance = 5;
        }
        if(distance > 30)
        {
            distance = 30;
        }
        Vector3 position = transform.position;
        position.x = orientation.transform.position.x;
        position.z = orientation.transform.position.z;
        position.y = orientation.transform.position.y + distance;
        transform.position = position;
        Vector3 mousePosition = Input.mousePosition;
        Debug.Log(mousePosition);



    }
    public void faceCursor()
    {
        Vector3 mousePose = Input.mousePosition;
        playerModel.transform.rotation = Quaternion.Euler(0, mousePose.y, 0);
    }
}
