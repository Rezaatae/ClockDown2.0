using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    private GameObject player;
    
    private bool gameHasEnded = false;
    
    public GameObject completeLevelUI;
    
    public void Start()
    {
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        int xPos = Random.Range(1, 4);          
        PhotonNetwork.Instantiate(Constants.Prefabs.Guy, new Vector3(xPos, 0.4f, -121f), Quaternion.identity);
    }


    public void Respawn() 
    {

        if(gameHasEnded == false && SceneManager.GetActiveScene().name != "Level 1")
        {
            gameHasEnded = true;
            Restart();
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        } 

    }


   private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel(){
        completeLevelUI.SetActive(true);
    }

    public void EndGame()
    {
        if(!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
        }
        
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.MainMenu);
        else
            SceneManager.LoadScene(Constants.Scenes.MainMenu);
                        
    }

}
