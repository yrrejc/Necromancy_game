using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicLongRangeEnemy : MonoBehaviour
{
    [Header("Componen")]
    public GameObject firePoint;
    public basicEnemyScript basicEnemyScriptRef;
    public GameObject Bullet;
    public int fireTime;
    public float fireTimeCountDown;
    public gameManagerScript gameManagerRef;
    [Header("settings")]
    public float fireDistance;
    [Header("Requirements")]
    public bool canFire;
    public bool firing;
    // Start is called before the first frame update
    void Start()
    {
        basicEnemyScriptRef = GetComponent<basicEnemyScript>();
        gameManagerRef = GameObject.FindObjectOfType<gameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        detectInRange();
        fire();
        if(canFire && !firing)
        {
            StartCoroutine(fireRate());
        }
       
    }

    void detectInRange()
    {
        if (basicEnemyScriptRef.targetDistance < fireDistance)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }
    }
    void fire()
    {
        if(!firing && canFire && gameManagerRef.attackToken <= gameManagerRef.maxAttackToken)
        {
            gameManagerRef.attackToken += 1;
            GameObject currentBullet = Instantiate(Bullet, transform.position, transform.rotation);
            Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(currentBullet.transform.forward * 90, ForceMode.Force);
            Debug.Log("fired");
        }
       
    }
    IEnumerator fireRate()
    {
        fireTime = Random.Range(20, 40);
        fire();
        firing = true ;
        yield return new WaitForSeconds(fireTime);
        firing = false;
    }
}
