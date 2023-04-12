using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxDetection : MonoBehaviour
{
    public Canvas canvas;

    private void OnTriggerEnter(Collider collider) {
        if (collider.name == "FirstPersonController") {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance <= 2f)
            {
                canvas.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
{
    if (collider.name == "FirstPersonController")
    {
        canvas.enabled = false;
    }
}
}
