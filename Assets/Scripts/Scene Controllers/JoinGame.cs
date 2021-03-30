using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class JoinGame : CreateJoinRoomUI
{

    public void OnClickJoinRoom()
    {
        actionButton.interactable = false;
        backButton.interactable = false;
        PhotonNetwork.JoinRoom(Text);
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene(Constants.Scenes.Game.Levels.GameLobby);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ShowPopup("Unable to join this room", LocalizePhotonErrorMessage.localizedMessage(returnCode));   
    }

}