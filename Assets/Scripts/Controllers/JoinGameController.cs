using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class JoinGameController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TMP_InputField roomNameInputField;

    [SerializeField]
    private Button joinRoomButton;

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
        joinRoomButton.interactable = true;
        Debug.Log("Unable to join this room: " + message);
    }

}