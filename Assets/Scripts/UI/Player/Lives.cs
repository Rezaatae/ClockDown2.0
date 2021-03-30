using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Lives : MonoBehaviour
{

    [SerializeField]
    private Text livesText;

    private GameManager gameManager;

    public int life;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        life = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining];
    }
    

    private void Update()
    {
        livesText.text = "LIVES: " + GetRemainingLives();

        if(life < 1){

            gameManager.EndGame();
        }
    }

    [PunRPC]
    public void Increase(int amount = 1)
    {
        life += amount;

        PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining] = life;
        PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    }

    public void Deduct()
    {
        life--;
        
        PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining] = life;
        PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    
    }

    private int GetRemainingLives()
    {
        return (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining];
    }

}
