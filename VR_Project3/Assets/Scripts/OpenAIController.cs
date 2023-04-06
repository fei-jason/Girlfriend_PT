using OpenAI_API;
using OpenAI_API.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using OpenAI_API.Models;

public class OpenAIController : MonoBehaviour
{

    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button okButton;
    public TextAsset contextTextFile;

    private OpenAIAPI api;
    // List of all messages including YOU and ChatGPT
    private List<ChatMessage> messages;

    // Start is called before the first frame update
    void Start()
    {
        // my api key, dont share it lol, kept the github private
        api = new OpenAIAPI("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");
        StartConversation();
        okButton.onClick.AddListener(() => GetResponse());

    }

    private void StartConversation()
    {
        // 3 roles in ChatMessageRole 
        // ChatMessageRole.System: Gives context in the beginning
        messages = new List<ChatMessage> { 
            new ChatMessage (ChatMessageRole.System, contextTextFile.text) };

        // starting string, we can change this later on when we meet Rosie for example
        inputField.text = "";
        string startString = "You have approached Rosie";
        textField.text = startString;
        Debug.Log(startString);

    }
    private async void GetResponse()
    {
       // dont send anything to chatgpt if our input is less than 1 or empty
       if (inputField.text.Length < 1)
        {
            return;
        }

        // Disable OK button
        okButton.enabled = false;

        // Fill the user message from the input field
        ChatMessage userMessage = new ChatMessage();
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
        ChatMessage responseMessage = new ChatMessage();
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
}
