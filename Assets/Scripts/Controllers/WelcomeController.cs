using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WelcomeController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TextMeshProUGUI _connectionStatusText;

    [SerializeField]
    private TMP_InputField _playerNickNameTextField;

    [SerializeField]
    private Button _continueButton;

    [SerializeField]
    private string _gameVersion;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
        _connectionStatusText.text = "Connecting to server...";
        _playerNickNameTextField.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(false);
        Debug.Log(PhotonNetwork.AppVersion);
    }

     public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the " + PhotonNetwork.CloudRegion + " server!");

        if (!PhotonNetwork.InLobby) 
        {
            PhotonNetwork.JoinLobby();
            _connectionStatusText.text = "Connecting to server...";
        }
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _connectionStatusText.text = "Disconnected from server." + cause.ToString();
    }

    public override void OnJoinedLobby()
    {
        _connectionStatusText.text = "Enter your name";
        _playerNickNameTextField.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(true);
    }

    public override void OnLeftLobby()
    {
        _connectionStatusText.text = "Left Lobby";
    }

    public void OnClick_Continue()
    {
        PhotonNetwork.NickName = _playerNickNameTextField.text;
        SceneManager.LoadScene(Constants.MainMenu);
    }

}