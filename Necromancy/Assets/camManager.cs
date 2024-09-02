using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camManager : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject playerRef;
    public GameObject playerModel;
    public bool usingCam1;

    void Start()
    {
        cam2();
    }

    void LateUpdate()
    {
        Rigidbody playerRB = playerRef.GetComponent<Rigidbody>();
        if (!usingCam1)
        {
            playerRB.angularVelocity = Vector3.zero;
        }
        // Update playerModel's position and rotation to match playerRef
        playerModel.transform.position = playerRef.transform.position;
        playerModel.transform.rotation = playerRef.transform.rotation;

        // Reset angular velocity and apply constraints based on active camera
        if (!usingCam1)
        {
            playerRB.angularVelocity = Vector3.zero;

            playerRB.constraints = RigidbodyConstraints.FreezeRotation;
            playerRB.angularDrag = 10f;
            // Align the player's rotation with the model's rotation (camera 2 mode)
            playerRef.transform.rotation = playerModel.transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            playerRB.angularVelocity = Vector3.zero;

            if (usingCam1)
            {
                cam2();
            }
            else
            {
                cam1();
            }
        }
    }

    public void cam1()
    {
        if (playerRef != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            camera1.SetActive(true);
            camera2.SetActive(false);
            Rigidbody playerRB = playerRef.GetComponent<Rigidbody>();
            playerRB.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            // No need to reset rotation here, just ensure constraints are correct
            usingCam1 = true;
        }
       
    }

    public void cam2()
    {
        if(playerRef != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            camera1.SetActive(false);
            camera2.SetActive(true);
            Rigidbody playerRB = playerRef.GetComponent<Rigidbody>();
            playerRB.constraints = RigidbodyConstraints.FreezeRotationY;
            playerRB.angularDrag = 10f;
            playerRB.angularVelocity = Vector3.zero;
            // Align the player model with the player ref for top-down view
            playerRef.transform.rotation = playerModel.transform.rotation;
            usingCam1 = false;
        }
      
    }
}