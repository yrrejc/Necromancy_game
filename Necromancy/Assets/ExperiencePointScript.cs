using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperiencePointScript : MonoBehaviour
{

    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void upDateExprienceBar(float currentExp, float maxExp)
    {
        slider.value = currentExp/maxExp;
    }


   


}
