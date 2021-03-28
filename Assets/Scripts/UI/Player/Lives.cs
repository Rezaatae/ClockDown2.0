using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lives : MonoBehaviour
{

    [SerializeField]
    private Text livesText;

    public int life;

    private void Start()
    {
        life = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentLifeRemaining];
    }
    

    private void Update()
    {
        livesText.text = "LIVES: " + GetRemainingLives();
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
