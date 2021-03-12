using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    bool gameHasEnded = false;

    private GameObject player;

    [SerializeField]
    private PhotonView playerPhotonView;
    
    // private int _whosTurn = 1;

    private ArrayList playerIds = new ArrayList();

    private int whosTurnIndex = 0;

    private bool isLocalPlayersTurn;


    public void Start()
    {

        GetPlayerIDs();
        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate(Constants.Prefabs.Stone, new Vector3(0,0,0), Quaternion.identity, 0);
            // player.AddComponent<GameManager>();
        } else 
        {
            PhotonNetwork.Instantiate(Constants.Prefabs.Stone, new Vector3(0,0,0), Quaternion.identity, 0);
        }

    }

    public void Update()
    {
        // if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(Constants.WhosTurnIndex) && PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("player_id"))
        // {
        //     whosTurnIndex = System.Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[Constants.WhosTurnIndex]);
        //     int playedId = System.Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerId]);

        //     isLocalPlayersTurn = playerPhotonView.IsMine && (int) playerIds[whosTurnIndex] == playedId;
        // }
    }

    public void GameOver() 
    {

        if(gameHasEnded == false && SceneManager.GetActiveScene().name == "SampleScene"){
            gameHasEnded = true;
            Debug.Log("Game Over");
            Restart();
        }
        else{
            SceneManager.LoadScene("SampleScene");
        } 

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsLocalPlayersTurn()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(Constants.WhosTurnIndex) && PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("player_id"))
        {
            whosTurnIndex = System.Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[Constants.WhosTurnIndex]);
            int playedId = System.Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerId]);

            return playerPhotonView.IsMine && (int) playerIds[whosTurnIndex] == playedId;
        }

        return false;
    }
    public void UpdateWhosTurn()
    {
        if (whosTurnIndex == PhotonNetwork.CurrentRoom.PlayerCount - 1)
            whosTurnIndex = 0;
            else 
                whosTurnIndex++;

        PhotonNetwork.CurrentRoom.CustomProperties[Constants.WhosTurnIndex] = whosTurnIndex;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
    }

    private void GetPlayerIDs()
    {
        playerIds.Clear();

        foreach(var player in PhotonNetwork.PlayerList)
        {
            int playedId = System.Convert.ToInt32(player.CustomProperties[Constants.PlayerId]);
            playerIds.Add(playedId);
        }

        Debug.Log(playerIds.Count);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(Constants.Scenes.MainMenu);
        } else
        {
            GetPlayerIDs();
            SceneManager.LoadScene(Constants.Scenes.MainMenu);
        }
                        
    }

}
