using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Splash : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject loadingIndicator;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private string gameVersion;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
        playButton.gameObject.SetActive(false);
    }

     public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the " + PhotonNetwork.CloudRegion + " server!");

        if (!PhotonNetwork.InLobby) 
        {
            PhotonNetwork.JoinLobby();
        }
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
    }

    public override void OnJoinedLobby()
    {
        playButton.gameObject.SetActive(true);
        loadingIndicator.gameObject.SetActive(false);
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(Constants.Scenes.NameSelection);
    }

}
