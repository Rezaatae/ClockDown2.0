using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateGameController : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TMP_InputField _roomNameInputField;

    [SerializeField]
    private Button _createGameButton;

    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte _maxPlayersPerRoom = 6;

    private void Start()
    {
        var colorBlock = _createGameButton.colors;
        colorBlock.disabledColor = Color.gray;
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = _maxPlayersPerRoom;
        PhotonNetwork.CreateRoom(_roomNameInputField.text, options, TypedLobby.Default);
        _createGameButton.interactable = false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _createGameButton.interactable = true;
        Debug.Log("failed to create room: " + message);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(Constants.GameLobby);
    }

}