using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    // [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject playerSpawnPosition;
    
    private bool gameHasEnded = false;
    
    public GameObject completeLevelUI;
    
    public void Start()
    {
        Debug.Log("New game manager");
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        // PhotonNetwork.Destroy(PhotonView.Get(player));
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
        player = PhotonNetwork.Instantiate(Constants.Prefabs.Player, new Vector3(Random.Range(-4, 0), playerSpawnPosition.transform.position.y, playerSpawnPosition.transform.position.z), Quaternion.identity);
        // DontDestroyOnLoad(player);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.MainMenu);
        else
            SceneManager.LoadScene(Constants.Scenes.MainMenu);
                        
    }

}
