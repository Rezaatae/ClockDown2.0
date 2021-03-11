using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // Hashtable playerProps = new Hashtable();
        // playerProps["player_id"] = PhotonNetwork.CurrentRoom.PlayerCount;
        // newPlayer.SetCustomProperties(playerProps);
    }

}