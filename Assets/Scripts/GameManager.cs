using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{

    bool gameHasEnded = false;

    private GameObject player;
    
    private ArrayList playerIds = new ArrayList();

    private int whosTurnIndex = 0;

    [SerializeField]
    private TextMeshProUGUI whosTurnText;


    public void Start()
    {
        // GetPlayerIDs();
        // if (PhotonNetwork.IsMasterClient)
        //     PhotonNetwork.Instantiate(Constants.Prefabs.Route, new Vector3(0,0,0), Quaternion.identity);
        PhotonNetwork.Instantiate(Constants.Prefabs.Guy, new Vector3(2,1,0), Quaternion.identity);
    }


    public void Respawn() 
    {

        if(gameHasEnded == false && SceneManager.GetActiveScene().name != "Level 1"){
            gameHasEnded = true;
            Restart();
        }
        else{
            SceneManager.LoadScene("Level 1");
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
            whosTurnText.text = "Player " + playerIds[whosTurnIndex] + "'s turn";
            return (int) playerIds[whosTurnIndex] == playedId;
        }

        return false;
    }

    public void EndGame(){
        if(gameHasEnded == false){
            gameHasEnded = true;
            Debug.Log("Game Over!");
        }
        
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
