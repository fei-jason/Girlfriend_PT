using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiftGiving : MonoBehaviour
{
    public TMP_InputField inputField;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gift")) 
        {
            PointerReal sendGift = new PointerReal();
            inputField.text = "*Gives you a nice juicy watermelon*";
            Debug.Log("Tried Gift");
            sendGift.GetResponse();
            Debug.Log("Successful Gift");
            inputField.text = "";
        }
    }
}
