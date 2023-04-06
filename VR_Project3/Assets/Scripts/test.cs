using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button okButton;
    public TextAsset textFile;
    public TextAsset textFile2;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        okButton.onClick.AddListener(() => GetResponse());
    }

    private async void GetResponse()
    {
        // STILL TESTING ALL OF THIS
        Debug.Log(textFile.text);
        textField.text = string.Format(textFile.text);

        Debug.Log(textFile2.text);

        String s = textFile2.text;

        s = s.Substring(s.IndexOf("{") + 1);
        s = s.Substring(0, s.IndexOf("}"));

        Debug.Log("Current intimacy is: " + s);
 
        String w = s.Remove(0, 9);
        w = w.Trim();
        Debug.Log(w);
    }
}
