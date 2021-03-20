using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject player;
    
    private bool gameHasEnded = false;
    
    public GameObject completeLevelUI;
    
    public void Start()
    {
        SpawnPlayer();
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
            SpawnPlayer();
        } 

    }


   private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
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

    private void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(Constants.Prefabs.Player, new Vector3(Random.Range(-4, 0), 0, 0), Quaternion.identity);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.MainMenu);
        else
            SceneManager.LoadScene(Constants.Scenes.MainMenu);
                        
    }

}
