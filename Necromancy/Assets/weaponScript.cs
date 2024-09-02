using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class weaponScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public int fireTime;
    public bool canFire;
    public int maxBullet;
    public int currentBullet;
    public bool reloading;
    public int reloadTime;
    public Text bulletCountText;
    // Start is called before the first frame update
    private void OnEnable() 
    {
        canFire = true;
    }
    void OnDisable(){
        canFire = false;
    }
    void Start()
    {
        currentBullet = maxBullet;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        bulletCountText.text = currentBullet + "/" + maxBullet;
        if(Input.GetKeyDown(KeyCode.Mouse0)&& canFire == true && currentBullet >= 0)
        {
            shoot();
        }
        if(Input.GetKeyDown(KeyCode.R) && canFire == true && currentBullet < maxBullet)
        {
            StartCoroutine("reload");
        }
    }

    void shoot()
    { 
        currentBullet -= 1;
        canFire = false;
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        StartCoroutine("fireTimeController");

    }
    IEnumerator fireTimeController()
    {
        
        yield return new WaitForSeconds(fireTime);
        canFire = true;
    }
    IEnumerator reload()
    {
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        currentBullet = maxBullet;
        canFire = true;

    }
}
