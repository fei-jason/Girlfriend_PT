using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

    private bool isFadingIn = true;
    private bool isFadingOut = false;

    float alpha = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (alpha > 0f && isFadingIn)
        {
            alpha = alpha - .005f;
            Color newColor = new Color(0f, 0f, 0f, alpha);
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }

        if (alpha < 1f && isFadingOut)
        {
            alpha = 1f;
            Color newColor = new Color(0f, 0f, 0f, alpha);
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }
	}

    public void SetIsFadingIn()
    {
        isFadingIn = true;
    }

    public void SetIsFadingOut()
    {
        isFadingIn = false;
        alpha = 1f;
        Color newColor = new Color(0f, 0f, 0f, alpha);
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
    }
}
