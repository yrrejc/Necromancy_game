using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnCollisionEnter(Collision other)
   {
    if(other.collider != null){
        Destroy(other.gameObject);
    }
   }
    void OnTriggerEnter(Collider other) {
    if(other.gameObject != null){
        Destroy(other.gameObject);
    }
   }
}
