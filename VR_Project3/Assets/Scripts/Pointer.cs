/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class Pointer : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    test test = new test();

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "OkButton")
        {
            test.GetResponse();
            Debug.Log("OkButton was clicked");
            
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "OkButton")
        {
            Debug.Log("RecordButton was entered");
        }

    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "OkButton")
        {
            Debug.Log("RecordButton was exited");
        }
    }
}