using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class rockGuyScript : MonoBehaviour
{
    public Animator animator;
    public basicEnemyScript enemyScriptRef;
    public int attackDistance;
    public int damage;
    public float attackCooldown;
    public float maxAttackCooldown;
    public Transform attackPoint;
    public bool canAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        maxAttackCooldown = 5;
        attackCooldown = maxAttackCooldown;
        canAttack = true;
        enemyScriptRef = gameObject. GetComponent<basicEnemyScript>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyScriptRef.walking == true)
        {
            animator.SetBool("Walk", true);
        }
        if(enemyScriptRef.walking == false)
        {
           
            animator.SetBool("Walk", false);
        }
        if(canAttack == false)
        {
            attackCooldown -= Time.deltaTime;
        }
        if(attackCooldown <=0)
        {
            canAttack = true;
            attackCooldown = maxAttackCooldown;
        }
       
        attackRange();
    }


    void attackRange()
    {
        Ray ray = new Ray(attackPoint.transform.position, attackPoint.transform.forward* attackDistance);
        Debug.DrawRay(ray.origin, ray.direction * attackDistance, Color.yellow);
        RaycastHit hit;
        LayerMask playerLayer = 1 << LayerMask.NameToLayer("player");

        if (Physics.Raycast(ray, out hit, attackDistance, playerLayer))
        {
            if(hit.collider.CompareTag("player") || hit.collider.CompareTag("minion"))
            {
                if(canAttack==true)
                {
                    canAttack = false;
                    Debug.Log("attack player roclman");
                    animator.SetTrigger("Attack");
                    
                }
            }
        }

        

    }
}
