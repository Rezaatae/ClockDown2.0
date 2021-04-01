using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    // // [SerializeField]
    // private GameObject player;

    // [SerializeField]
    // private GameObject playerSpawnPosition;
    
    private bool gameHasEnded = false;
    
    public GameObject completeLevelUI;
    public GameObject GameOverUI;
    
    public void Start()
    {
        // SpawnPlayer();
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
            // SpawnPlayer();
        } 

    }


   private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reloading current active scene on respawn
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true); // activating Time Up UI when time is up
    }

    public void EndGame()
    {
        if(!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            GameOverUI.SetActive(true);
        }
        
    }

    // private void SpawnPlayer()
    // {
    //     player = PhotonNetwork.Instantiate(Constants.Prefabs.Player, new Vector3(Random.Range(-4, 0), playerSpawnPosition.transform.position.y, playerSpawnPosition.transform.position.z), Quaternion.identity);
    //     // DontDestroyOnLoad(player);
    // }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.Game.MainMenu);
        else
            SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
                        
    }

}
