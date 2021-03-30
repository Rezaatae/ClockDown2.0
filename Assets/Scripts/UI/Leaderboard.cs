using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Gravitons.UI.Modal;

public class Leaderboard : MonoBehaviour
{

    [SerializeField]
    private Button backToGameLobbyButton;

	void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            backToGameLobbyButton.gameObject.SetActive(true);
        else
            backToGameLobbyButton.gameObject.SetActive(false);

        

	}

	public void OnClickBackToLobby()
    {
            ModalManager.Show(null, "You're about to enter the game lobby", new[] { new ModalButton() { Callback = BackToGameLobby, Text = "Confirm" }, new ModalButton() { Text = "Cancel" } });
    }

    private void BackToGameLobby()
    {
        PhotonNetwork.LoadLevel(Constants.Scenes.Game.Levels.GameLobby);
    }

	public void OnClickLeaveGame()
    {
            ModalManager.Show(null, "You're about to leave the game", new[] { new ModalButton() { Callback = LeaveGame, Text = "Confirm" }, new ModalButton() { Text = "Cancel" } });
    }

    private void LeaveGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.Game.MainMenu);
        else
            SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
        PhotonNetwork.LeaveRoom();
    }

}
