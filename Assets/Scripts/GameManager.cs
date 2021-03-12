using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{

    bool gameHasEnded = false;

    private GameObject player;

    private GameObject route;

    private int _whosTurn = 1;

    private ArrayList playerIds;

    public void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate(Constants.Prefabs.Stone, new Vector3(0,0,0), Quaternion.identity, 0);
            route = PhotonNetwork.Instantiate(Constants.Prefabs.Route, new Vector3(0,0,0), Quaternion.identity, 0);
        } else 
        {
            PhotonNetwork.Instantiate(Constants.Prefabs.Stone, new Vector3(0,0,0), Quaternion.identity, 0);
        }

    }

    public void GameOver(){

        if(gameHasEnded == false && SceneManager.GetActiveScene().name == "SampleScene"){
            gameHasEnded = true;
            Debug.Log("Game Over");
            Restart();
        }
        else{
            SceneManager.LoadScene("SampleScene");
        } 

    }

    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.MainMenu);
    }

}
