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
        Debug.Log("Start");
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "0.0.1";
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
        _connectingToServerText.text = "Disconnected from server." + cause.ToString();
    }

    public override void OnJoinedLobby()
    {
        _connectingToServerText.text = "Joined Lobby";
    }

    public override void OnLeftLobby()
    {
        _connectingToServerText.text = "Left Lobby";
    }

    public void OnClick_CreateRoom()
    {
        SceneManager.LoadScene("CreateGame");
    }

    public void OnClick_JoinRoom()
    {
        SceneManager.LoadScene("JoinGame");
    }

}
