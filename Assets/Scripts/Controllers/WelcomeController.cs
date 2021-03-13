using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WelcomeController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TextMeshProUGUI connectionStatusText;

    [SerializeField]
    private GameObject inputPanel;

    [SerializeField]
    private TMP_InputField playerNickNameTextField;

    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private string gameVersion;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
        connectionStatusText.text = "Connecting to server...";
        inputPanel.SetActive(false);
        Debug.Log(PhotonNetwork.AppVersion);
    }

     public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the " + PhotonNetwork.CloudRegion + " server!");

        if (!PhotonNetwork.InLobby) 
        {
            PhotonNetwork.JoinLobby();
            connectionStatusText.text = "Connecting to server...";
        }
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionStatusText.text = "Disconnected from server." + cause.ToString();
    }

    public override void OnJoinedLobby()
    {
        connectionStatusText.text = "Enter your name";
        inputPanel.SetActive(true);
    }

    public override void OnLeftLobby()
    {
        connectionStatusText.text = "Left Lobby";
    }

    public void OnClickContinue()
    {
        PhotonNetwork.NickName = playerNickNameTextField.text;
        SceneManager.LoadScene(Constants.Scenes.MainMenu);
    }

}