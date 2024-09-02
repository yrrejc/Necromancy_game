using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentSuicide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.childCount < 1){
            Destroy(gameObject);
        }
    }
}
