using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    bool gameHasEnded = false;

    private GameObject player;

    public void Start()
    {

        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
        Debug.Log("Connected to master");
    }

    public override void OnJoinedLobby()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom("marvin", options, TypedLobby.Default);
        Debug.Log("Joined room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Stone", new Vector3(0,0,0), Quaternion.identity, 0);
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
}
