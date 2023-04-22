using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using OpenAI;


public class test : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button okButton;
    public TextAsset textFile;
    public TextAsset textFile2;
    public Slider slider;

    // extras
    [SerializeField] private Button recordButton;
    [SerializeField] private Image progressBar;


    // voice to text
    private OpenAIApi openai = new OpenAIApi("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");
    private readonly string fileName = "output.wav";
    private readonly int duration = 3;
    private float startRecordingTime;
    public AudioClip clip;
    public AudioSource audioSource;
    private bool isRecording;
    private float time;


    //public var index;



    // Start is called before the first frame update
    void Start()
    {
        okButton.onClick.AddListener(() => GetResponse());
        audioSource = GetComponent<AudioSource>();

        //index = PlayerPrefs.GetInt("user-mic-device-index");
        //Debug.Log(index);
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        recordButton.onClick.AddListener(StartRecording);

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

    private void StartRecording()
    {
        isRecording = true;
        recordButton.enabled = false;

        clip = Microphone.Start(Microphone.devices[0], false, duration, 44100);
    }

    private async void EndRecording()
    {
        Microphone.End(null);
        byte[] data = SaveWav.Save(fileName, clip);

        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            // File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
            Language = "en"
        };
        var res = await openai.CreateAudioTranscription(req);

        progressBar.fillAmount = 0;
        textField.text = res.Text;
        recordButton.enabled = true;
    }

    void Update()
    {
        if (isRecording)
        {
            time += Time.deltaTime;
            progressBar.fillAmount = time / duration;

            if (time >= duration)
            {
                time = 0;
                isRecording = false;
                EndRecording();
            }
        }
    }


}
