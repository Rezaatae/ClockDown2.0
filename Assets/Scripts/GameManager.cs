using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    bool gameHasEnded = false;

    // private GameObject player;

    public void Start()
    {

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

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("disconnected: " + cause.ToString());
    }
}
