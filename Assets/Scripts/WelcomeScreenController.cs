using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WelcomeScreenController : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TextMeshProUGUI _connectingToServerText;

    private void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        _connectingToServerText.text = "Connecting to server...";
    }

     public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the " + PhotonNetwork.CloudRegion + " server!");

        if (!PhotonNetwork.InLobby) 
        {
            PhotonNetwork.JoinLobby();
            _connectingToServerText.text = "Connecting to server...";
        }
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _connectingToServerText.text = "Disconnected from server.";
        // _inputNickNameCanvas.gameObject.SetActive(false);
    }

    public override void OnJoinedLobby()
    {
        _connectingToServerText.text = "Joined Lobby";
        // _inputNickNameCanvas.gameObject.SetActive(true);
        // _loadingGameCanvas.gameObject.SetActive(false);
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom("marvin", options, TypedLobby.Default);
        Debug.Log("Joined room");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room: " + message);
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom("marvin");
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room");
    }

}
