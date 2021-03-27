using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NewGameController : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TMP_InputField roomNameInputField;

    [SerializeField]
    private Button createGameButton;

    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte maxPlayersPerRoom;

    private void Start()
    {
        var colorBlock = createGameButton.colors;
        colorBlock.disabledColor = Color.gray;
    }

    public void OnClickCreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayersPerRoom;
        PhotonNetwork.CreateRoom(roomNameInputField.text, options, TypedLobby.Default);
        createGameButton.interactable = false;
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        createGameButton.interactable = true;
        Debug.Log("failed to create room: " + message);
    }

    public override void OnJoinedRoom()
    {
        Hashtable playerProps = new Hashtable();
        playerProps[Constants.PlayerId] = 1;
        PhotonNetwork.SetPlayerCustomProperties(playerProps);

        SceneManager.LoadScene(Constants.Scenes.Game.Levels.GameLobby);
    }

}