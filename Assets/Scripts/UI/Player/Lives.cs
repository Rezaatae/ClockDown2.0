using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lives : MonoBehaviour
{

    [SerializeField]
    private Text livesText;

    private int life;
    private int localLife = 5;

    private void Start()
    {
        life = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining];
    }
    

    private void Update()
    {
        livesText.text = "LIVES: " + GetRemainingLives();
    }

    public void Increase(int amount = 1)
    {
        life += amount;
        localLife += amount;

        PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining] = life;
        PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    }

    public void Deduct()
    {
        life--;
        localLife--;
        
        if (localLife == 0)
        {
            gameOver();
        }

        PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining] = life;
        PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    
    }

    private int GetRemainingLives()
    {
        return (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining];
    }

    public void gameOver()
    {
        Player playerStats = GetComponent<Player>();
        playerStats.playerAlive = false;
    }
    

}
