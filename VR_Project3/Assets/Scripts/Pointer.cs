/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using TMPro;
using OpenAI;

public class Pointer : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    public TMP_Text textField;
    public TMP_InputField inputField;
    public TextAsset textFile;
    public TextAsset textFile2;
    public Slider slider;


    // extras
    [SerializeField] private Button recordButton;
    [SerializeField] private Image progressBar;

    // speech to text
    private OpenAIApi openai = new OpenAIApi("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");
    private readonly string fileName = "output.wav";
    private readonly int duration = 3;
    private float startRecordingTime;
    public AudioClip clip;
    public AudioSource audioSource;
    private bool isRecording;
    private float time;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //index = PlayerPrefs.GetInt("user-mic-device-index");
        //Debug.Log(index);
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        //recordButton.onClick.AddListener(StartRecording);

    }

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        switch (e.target.name)
        {
            case "Cube":
                Debug.Log("Cube was clicked");
                GetResponse();
                break;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        switch (e.target.name)
        {
            case "Cube":
                laserPointer.color = Color.yellow;
                break;
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        switch (e.target.name)
        {
            case "Cube":
                laserPointer.color = Color.black;
                break;
        }
    }

        public async void GetResponse()
    {
        // STILL TESTING ALL OF THIS
//        Debug.Log(textFile.text);
        textField.text = string.Format(textFile.text);

        Debug.Log(textFile2.text);

        string s = textFile2.text;

        s = s.Substring(s.IndexOf("{") + 1);
        s = s.Substring(0, s.IndexOf("}"));

        Debug.Log("Current intimacy is: " + s);
 
        string w = s.Remove(0, 9);
        w = w.Trim();
        Debug.Log(w);
    }

    public void StartRecording()
    {
        isRecording = true;
        recordButton.enabled = false;

        clip = Microphone.Start(Microphone.devices[0], false, duration, 44100);
    }

    public async void EndRecording()
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