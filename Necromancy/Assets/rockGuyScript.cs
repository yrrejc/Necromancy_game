using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockGuyScript : MonoBehaviour
{
    public Animator animator;
    public basicEnemyScript enemyScriptRef;
    // Start is called before the first frame update
    void Start()
    {
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
    }
}
