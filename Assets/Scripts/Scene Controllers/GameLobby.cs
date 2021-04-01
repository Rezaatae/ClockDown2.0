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
        // When a lobby is created, a hashtable is made setting all player scores 0 and player lives to 5.
        // These values are then synchronised across the network.

        Hashtable dict = new Hashtable();
        dict[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining] = 5;
        dict[Constants.Scenes.Game.Objects.PlayerCurrentScore] = 0;
        PhotonNetwork.SetPlayerCustomProperties(dict);

        // The name of the room is displayed across all player's screens.

        roomNameText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
    }

    /* 
    This Update method controls the accessibility of the starting the game based on how many players are in the lobby.
    If there are 2 or more players in a lobby, the button allowing players to enter the game on the hosts screen becomes enabled.
    If there are less than 2 players, then the lobby will tell the users that they're waiting for more players.
    */
    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
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
            gameStatusText.text = "Waiting for more players. Game requires at least 2 players";
            startGameButton.interactable = false;
        }

    }

    // When the start game button is clicked, Photon will load Level 1 on all player screens
    public void OnClickStartGame()
    {
        PhotonNetwork.LoadLevel(Constants.Scenes.Game.Levels.Level1);
    }

    // If a player wants to leave the lobby and clicks the leave button, a pop up message will appear asking to confirm this decision.
    public void OnClickLeaveGame()
    {
            ModalManager.Show(null, "You're about to leave the game", new[] { new ModalButton() { Callback = LeaveGame, Text = "Confirm" }, new ModalButton() { Text = "Cancel" } });
    }

    // If the player confirms this, they will return to the menu and leave the lobby
    // Otherwise, they will stay in the lobby

    public void LeaveGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.Game.MainMenu);
            else
                SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
        PhotonNetwork.LeaveRoom();
    }

    /* 
    This creates a hashtable of playerIDs currently in the room. When a new id is added, the player count increases and
    the debug log will notify the user that a 'playerid' has joined
    */
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Hashtable playerProps = new Hashtable();
        playerProps[Constants.Scenes.Game.Objects.PlayerId] = PhotonNetwork.CurrentRoom.PlayerCount;
        newPlayer.SetCustomProperties(playerProps);
        Debug.Log(newPlayer.NickName + " just joined the game");
    }

    // When a player leaves the room, the network will notify the user that 'playerid' has left the game
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " just left the game");
    }

}
