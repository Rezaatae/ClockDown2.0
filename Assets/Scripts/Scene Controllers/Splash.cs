using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Gravitons.UI.Modal;
using System.Collections;

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
        
        ModalManager.Show("You're disconnected from the game", LocalizePhotonErrorMessage.disconnectCause(cause), new[] { new ModalButton() { Text = "YES", Callback = RetryPhotonNetwork }, new ModalButton() { Text = "NO" } });
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

    private void RetryPhotonNetwork()
    {
        StartCoroutine(MainReconnect());
    }

    private IEnumerator MainReconnect()
    {
        while (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != ExitGames.Client.Photon.PeerStateValue.Disconnected)
        {
            yield return new WaitForSeconds(0.2f);
        }

        loadingIndicator.gameObject.SetActive(true);

        if (!PhotonNetwork.Reconnect())
        {
            playButton.gameObject.SetActive(false);
            loadingIndicator.gameObject.SetActive(false);
            ModalManager.Show("Unable to connect to game", "There was an error connecting to our servers", new[] { new ModalButton() { Text = "OK" } });
        } else
        {
            playButton.gameObject.SetActive(true);
            loadingIndicator.gameObject.SetActive(false);
        }
    }

}
