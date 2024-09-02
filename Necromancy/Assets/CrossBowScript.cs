using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowScript : MonoBehaviour
{

    public bool fireKey;
    public bool canFire;
    public Animator animator;
    public GameObject arrow;
    public GameObject arrowLocation;
    public Camera cam;
    public Vector3 direction;
    public GameObject UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cam = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UICanvas");
        fireKey = Input.GetKeyDown(KeyCode.Mouse0);
        if (fireKey && UICanvas == null&&canFire)
        {
            raycastFirePoint();
            animator.SetTrigger("Fire");
            canFire = false;
        }
        

    }

    public void raycastFirePoint()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
        {
            targetPoint = ray.GetPoint(300);
        }
        Debug.DrawRay(ray.origin, ray.direction * 75f, Color.red, 2f);
        direction = arrowLocation.transform.position - targetPoint;


    }
    public void shootArrow()
    {
        if(arrowLocation != null)
        {
            GameObject currenArrow = Instantiate(arrow, arrowLocation.transform.position, Quaternion.identity);
            currenArrow.transform.forward = -direction;
            Rigidbody arrowRb = currenArrow.GetComponent<Rigidbody>();
            arrowRb.AddForce(-transform.forward * 10, ForceMode.Impulse);
            Debug.Log("fireed");
        }
        
    }

    public void reload()
    {
        canFire = true;
    }
}
