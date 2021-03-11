using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    bool gameHasEnded = false;

    private GameObject player;

    private GameObject route;

    public void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate("Stone", new Vector3(0,0,0), Quaternion.identity, 0);
            route = PhotonNetwork.Instantiate("Route", new Vector3(0,0,0), Quaternion.identity, 0);
        } else 
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

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.MainMenu);
    }

}
