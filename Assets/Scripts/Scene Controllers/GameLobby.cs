using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Gravitons.UI.Modal;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameLobby : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TextMeshProUGUI gameStatusText;

    [SerializeField]
    private TextMeshProUGUI roomNameText;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private Button leaveGameButton;

    private void Start()
    {
        Hashtable dict = new Hashtable();
        dict[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining] = 5;
        dict[Constants.Scenes.Game.Objects.PlayerCurrentScore] = 0;
        PhotonNetwork.SetPlayerCustomProperties(dict);

        roomNameText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                gameStatusText.text = PhotonNetwork.CurrentRoom.PlayerCount + " players are currently in the game";
                startGameButton.interactable = true;
            } else 
            {
                gameStatusText.text = "Waiting for room owner to start the game";
                startGameButton.gameObject.SetActive(false);
            }
        } else
        {
            gameStatusText.text = "Waiting for more players. Game requires atleast 2 players";
            startGameButton.interactable = false;
        }

    }

    public void OnClickStartGame()
    {
        PhotonNetwork.LoadLevel(Constants.Scenes.Game.Levels.Level1);
    }

    public void OnClickLeaveGame()
    {
            ModalManager.Show(null, "You're about to leave the game", new[] { new ModalButton() { Callback = LeaveGame, Text = "Confirm" }, new ModalButton() { Text = "Cancel" } });
    }

    public void LeaveGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.Game.MainMenu);
            else
                SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Hashtable playerProps = new Hashtable();
        playerProps[Constants.Scenes.Game.Objects.PlayerId] = PhotonNetwork.CurrentRoom.PlayerCount;
        newPlayer.SetCustomProperties(playerProps);
        Debug.Log(newPlayer.NickName + " just joined the game");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " just left the game");
    }

}
