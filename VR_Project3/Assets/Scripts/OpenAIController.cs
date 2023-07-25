using OpenAI_API;
using OpenAI_API.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using OpenAI_API.Models;
using OpenAI;

public class OpenAIController : MonoBehaviour
{

    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button okButton;
    public TextAsset contextTextFile;
    
    // Whisper
    [SerializeField] private Button recordButton;
    [SerializeField] private Image progressBar;
    private readonly string fileName = "output.wav";
    private readonly int duration = 3;
    private AudioClip clip;
    private bool isRecording;
    private float time;
    private OpenAIApi openai = new OpenAIApi("");
    private OpenAIAPI api;


    // List of all messages including YOU and ChatGPT
    private List<OpenAI_API.Chat.ChatMessage> messages;

    // Start is called before the first frame update
    void Start()
    {
        // my api key, dont share it lol, kept the github private
        api = new OpenAIAPI("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");
        StartConversation();
        okButton.onClick.AddListener(() => GetResponse());
        recordButton.onClick.AddListener(StartRecording);
    }

    public void StartConversation()
    {
        // 3 roles in ChatMessageRole 
        // ChatMessageRole.System: Gives context in the beginning
        messages = new List<OpenAI_API.Chat.ChatMessage> { 
            new OpenAI_API.Chat.ChatMessage (ChatMessageRole.System, contextTextFile.text) };

        // starting string, we can change this later on when we meet Rosie for example
        inputField.text = "";
        string startString = "You have approached Rosie";
        textField.text = startString;
        Debug.Log(startString);

    }
    public async void GetResponse()
    {
       // dont send anything to chatgpt if our input is less than 1 or empty
       if (inputField.text.Length < 1)
        {
            return;
        }

        // Disable OK button
        okButton.enabled = false;

        // Fill the user message from the input field
        OpenAI_API.Chat.ChatMessage userMessage = new OpenAI_API.Chat.ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = inputField.text;

        if (userMessage.Content.Length > 100)
        {
            // limit messages to 100 characters.
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0} : {1}", userMessage.rawRole, userMessage.Content));

        // Add the message to the List
        messages.Add(userMessage);

        // Update the text field with user message
        textField.text = string.Format("You: {0}", userMessage.Content);

        // Clear input field
        inputField.text = "";

        // send the entire chat to OpenAI to get next message
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            // chose a Model!
            Model = Model.ChatGPTTurbo,
            // temperature is uniqueness or creativeness (lots of incorrectness)
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = messages
        });

        // Get response from ChatGPT
        OpenAI_API.Chat.ChatMessage responseMessage = new OpenAI_API.Chat.ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0} : {1}", responseMessage.rawRole, responseMessage.Content));

        // Add response to list of messages
        messages.Add(responseMessage);

        // Update the text field with response
        textField.text = string.Format("You: {0}\n\n{1}", userMessage.Content, responseMessage.Content);

        // Re-enable the OK button
        okButton.enabled = true;
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
        inputField.text = res.Text;
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
