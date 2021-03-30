using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Gravitons.UI.Modal;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NewGame : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TMP_InputField roomNameInputField;

    [SerializeField]
    private Button createGameButton;

    [SerializeField]
    private Button backButton;

    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte maxPlayersPerRoom;

    private void Update()
    {
        if (roomNameInputField.text.Length == 0 || roomNameInputField.text == " ")
        {
            createGameButton.interactable = false;
        } else
        {
            createGameButton.interactable = true;
        }
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
        createGameButton.interactable = false;
        backButton.interactable = false;
        ModalManager.Show("Failed to create room", LocalizePhotonErrorMessage.localizedMessage(returnCode), new[] { new ModalButton() { Callback = EnableButtons, Text = "OK" } });
    }

    public override void OnJoinedRoom()
    {
        Hashtable playerProps = new Hashtable();
        playerProps[Constants.Scenes.Game.Objects.PlayerId] = 1;
        PhotonNetwork.SetPlayerCustomProperties(playerProps);

        SceneManager.LoadScene(Constants.Scenes.Game.Levels.GameLobby);
    }

    private void EnableButtons()
    {
        createGameButton.interactable = true;
        backButton.interactable = true; 
    }

}