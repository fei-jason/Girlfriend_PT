using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{

    public Canvas canvas;
    public Canvas playerCanvas;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value >= 0.3) 
        {
            canvas.enabled = true;
            playerCanvas.enabled = false;
        }
    }
}
