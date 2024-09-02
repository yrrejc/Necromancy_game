using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingHealthBar : MonoBehaviour
{
    public Slider slider;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //cam = FindObjectOfType<camController>().cam;c
        //cam = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        //transform.parent.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
    public void updateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;
    }
}
