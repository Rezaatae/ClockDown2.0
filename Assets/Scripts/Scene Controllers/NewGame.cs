using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NewGame : CreateJoinRoomUI
{
    
    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte maxPlayersPerRoom;

    public void OnClickCreateRoom()
    {
        actionButton.interactable = false;
        backButton.interactable = false;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayersPerRoom;
        PhotonNetwork.CreateRoom(Text, options, TypedLobby.Default);
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
        ShowPopup("Failed to create room", LocalizePhotonErrorMessage.localizedMessage(returnCode));
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.Levels.GameLobby);
    }

}