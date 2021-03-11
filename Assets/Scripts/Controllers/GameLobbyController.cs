using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLobbyController : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TextMeshProUGUI _gameStatusText;

    [SerializeField]
    private Button _loadArenaButton;

    [SerializeField]
    private Button _leaveArenaButton;

    private byte _totalPlayersInGameLobby = 0;

    private void Start()
    {
        var loadArenaButtonColorBlock = _loadArenaButton.colors;
        loadArenaButtonColorBlock.disabledColor = Color.gray;
        var leaveArenaButtonColorBlock = _leaveArenaButton.colors;
        leaveArenaButtonColorBlock.disabledColor = Color.gray;
    }

    private void Update()
    {

        if (_totalPlayersInGameLobby > 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _gameStatusText.text = "All players are here, press continue";
                _loadArenaButton.interactable = true;
            } else 
            {
                _gameStatusText.text = "Waiting for player 1 to load the arena";
                _loadArenaButton.gameObject.SetActive(false);
            }
        } else
        {
            _gameStatusText.text = "Waiting for more players";
            _loadArenaButton.interactable = false;
        }

    }

    public void OnClick_CharacterSelection()
    {
        Debug.Log("Going to load character selection scene");
    }

    public void OnClick_LoadArena()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            PhotonNetwork.LoadLevel(Constants.MainMenu);
    }

    public void OnClick_LeaveRoom()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.MainMenu);
            else
                SceneManager.LoadScene(Constants.MainMenu);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        _totalPlayersInGameLobby++;
        Debug.Log(newPlayer.NickName + " just joined the game");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        _totalPlayersInGameLobby--;
        Debug.Log(otherPlayer.NickName + " just left the game");
    }

}
