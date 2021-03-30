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

    private void Update()
    {
        if (String.IsNullOrWhiteSpace(inputField.text))
            actionButton.interactable = false;
        else
            Text = inputField.text;
            actionButton.interactable = true;
    }

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