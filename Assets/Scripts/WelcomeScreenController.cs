using UnityEngine;
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

}
