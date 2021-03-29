using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Gravitons.UI.Modal;

public class JoinGame : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TMP_InputField roomNameInputField;

    [SerializeField]
    private Button joinRoomButton;

    [SerializeField]
    private Button backButton;

    public void OnClickJoinRoom()
    {
        joinRoomButton.interactable = false;
        PhotonNetwork.JoinRoom(roomNameInputField.text);
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene(Constants.Scenes.Game.Levels.GameLobby);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        joinRoomButton.interactable = false;
        backButton.interactable = false;
        ModalManager.Show("Unable to join this room", LocalizePhotonErrorMessage.localizedMessage(returnCode), new[] { new ModalButton() { Callback = EnableButtons, Text = "OK" } });
        
    }

    private void EnableButtons()
    {
        joinRoomButton.interactable = true;
        backButton.interactable = true; 
    }

}