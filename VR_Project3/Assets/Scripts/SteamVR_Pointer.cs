/* SceneHandler.cs*//*
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "RecordButton")
        {
            Debug.Log("RecordButton was clicked");
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "RecordButton")
        {
            Debug.Log("RecordButton was entered");
        }
        
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "RecordButton")
        {
            Debug.Log("RecordButton was exited");
        }
    }
}*/