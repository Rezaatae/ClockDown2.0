using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class JoinGameController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TextMeshProUGUI _roomNameInputField;

    [SerializeField]
    private Button _joinRoomButton;

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.NickName = "asilah";
        _joinRoomButton.interactable = false;
        PhotonNetwork.JoinRoom(_roomNameInputField.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene(Constants.GameLobby);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _joinRoomButton.interactable = true;
        Debug.Log("Unable to join this room: " + message);
    }

}