using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class JoinGameController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TMP_InputField _roomNameInputField;

    [SerializeField]
    private Button _joinRoomButton;

    public void OnClick_JoinRoom()
    {
        _joinRoomButton.interactable = false;
        Debug.Log(_roomNameInputField.text);
        PhotonNetwork.JoinRoom(_roomNameInputField.text);
    }

    public void OnClick_BackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.MainMenu);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene(Constants.Scenes.GameLobby);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _joinRoomButton.interactable = true;
        Debug.Log("Unable to join this room: " + message);
    }

}