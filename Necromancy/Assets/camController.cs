using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class camController : MonoBehaviour
{
    [Header("mouseSetting")]
    public float MouseSens;
    public float xRotation;
    public float yRotation;
    [Header("Component")]
    public Transform orientation;
    public UImanagerScript UImanagerScriptRef;
    public Camera cam;
    public TextMeshProUGUI textMeshPro;
    public LevelGeneratorScript levelGeneratorScriptRef;
    public GameObject levelGeneratorRef;
    public GameObject playerRef;
    [Header("Intereaction")]
    public bool doorIntereact;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      //  textMeshPro.enabled = false;
    }
    private void OnDisable()
    {
        Rigidbody playerRb = playerRef.GetComponent<Rigidbody>();
        playerRb.angularVelocity = Vector3.zero;
        playerRb.angularDrag = 1000;
    }
    // Update is called once per frame
    void Update()
    {
        levelGeneratorScriptRef = GameObject.FindAnyObjectByType<LevelGeneratorScript>();

        //intereact();
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSens * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    }
    private void LateUpdate()
    {
        UImanagerScriptRef = GameObject.FindAnyObjectByType<UImanagerScript>();
        if (UImanagerScriptRef.usingUi == false&& UImanagerScriptRef!=null)
        {
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        
    }

    void intereact()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1);
        if(Physics.Raycast(ray, out hit, 1))
        {
            if (hit.collider.CompareTag("door"))
            {
                textMeshPro.text = "Open Door";
                textMeshPro.enabled = true;
                doorIntereact = true;
            }
        }
        else
        {
            textMeshPro.enabled=false;
            doorIntereact =false;
        }
        //

        if(doorIntereact == true && Input.GetKeyDown(KeyCode.E) && levelGeneratorScriptRef.enemyAlive == 0&&levelGeneratorRef!=null)
        {
            levelGeneratorScriptRef.makeNewRoom();
            levelGeneratorScriptRef.gameObject.GetComponentInChildren<Animator>().SetTrigger("openDoor");
            //cam.enabled = false;
            //Invoke("reEnableCam", 1f);
        }
    }

    public void reEnableCam()
    {
        cam.enabled = true;
    }

    
}
