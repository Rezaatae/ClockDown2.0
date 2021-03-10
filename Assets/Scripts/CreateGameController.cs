using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateGameController : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TextMeshProUGUI _roomNameInputField;

    [SerializeField]
    private Button _createGameButton;

    [SerializeField]
    private Button _loadArenaButton;

    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte _maxPlayersPerRoom = 4;

    private void Start()
    {
        var colorBlock = _createGameButton.colors;
        colorBlock.disabledColor = Color.gray;
        _roomNameInputField.gameObject.SetActive(true);
        _createGameButton.gameObject.SetActive(true);
        _loadArenaButton.gameObject.SetActive(false);
    }

    public void OnClick_LoadArena()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel("Main Scene");
        } else {
            Debug.Log("must be 2 or more players to load arena");
        }
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.NickName = "kauna";
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = _maxPlayersPerRoom;
        _createGameButton.interactable = false;
        PhotonNetwork.CreateRoom(_roomNameInputField.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _createGameButton.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        // SceneManager.LoadScene("Main Scene");
        _roomNameInputField.gameObject.SetActive(false);
        _createGameButton.gameObject.SetActive(false);
        _loadArenaButton.gameObject.SetActive(true);
    }

}