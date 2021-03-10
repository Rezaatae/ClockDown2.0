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

    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte _maxPlayersPerRoom = 4;

    private void Start()
    {
        var colorBlock = _createGameButton.colors;
        colorBlock.disabledColor = Color.gray;
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
        SceneManager.LoadScene("Main Scene");
    }

}