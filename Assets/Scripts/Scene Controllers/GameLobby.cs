using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameLobby : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TextMeshProUGUI gameStatusText;

    [SerializeField]
    private Button loadArenaButton;

    [SerializeField]
    private Button leaveArenaButton;

    private void Start()
    {
        var loadArenaButtonColorBlock = loadArenaButton.colors;
        loadArenaButtonColorBlock.disabledColor = Color.gray;
        var leaveArenaButtonColorBlock = leaveArenaButton.colors;
        leaveArenaButtonColorBlock.disabledColor = Color.gray;

        Hashtable dict = new Hashtable();
        dict[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining] = 5;
        dict[Constants.Scenes.Game.Objects.PlayerCurrentScore] = 0;
        PhotonNetwork.SetPlayerCustomProperties(dict);
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                gameStatusText.text = "All players are here, press continue";
                loadArenaButton.interactable = true;
            } else 
            {
                gameStatusText.text = "Waiting for player 1 to load the arena";
                loadArenaButton.gameObject.SetActive(false);
            }
        } else
        {
            gameStatusText.text = "Waiting for more players";
            loadArenaButton.interactable = false;
        }

    }

    public void OnClickCharacterSelection()
    {
        Debug.Log("Going to load character selection scene");
    }

    public void OnClickLoadArena()
    {
        PhotonNetwork.LoadLevel(Constants.Scenes.Game.Levels.Level1);
    }

    public void OnClickLeaveRoom()
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
