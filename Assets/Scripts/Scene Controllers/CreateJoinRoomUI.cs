using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Gravitons.UI.Modal;
using System;

public class CreateJoinRoomUI : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TextMeshProUGUI inputField;

    [SerializeField]
    public Button actionButton;

    [SerializeField]
    public Button backButton;

    public string Text { get; private set; }

    // If the input field is null/contains no characters, the button is disabled
    private void Update()
    {
        if (String.IsNullOrWhiteSpace(inputField.text))
            actionButton.interactable = false;
        else
            Text = inputField.text;
            actionButton.interactable = true;
    }

    // If there is text in the input field, the button becomes enabled
    private void EnableButtons()
    {
        actionButton.interactable = true;
        backButton.interactable = true; 
    }

    public void ShowPopup(string title, string message)
    {
        ModalManager.Show(title, message, new[] { new ModalButton() { Callback = EnableButtons, Text = "OK" } });   
    }

}