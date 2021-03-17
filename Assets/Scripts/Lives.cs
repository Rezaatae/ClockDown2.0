using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lives : MonoBehaviour
{

    [SerializeField]
    private Text livesText;

    private int life;

    [SerializeField]
    private PhotonView player;
    
    private void Start()
    {
        life = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentLifeRemaining];
    }
    

    // Update is called once per frame
    private void Update()
    {
        if (player.IsMine)
            livesText.text = "LIVES: " + GetRemainingLives();
    }

    public void Deduct()
    {
        life--;

        if (player.IsMine)
        {
            PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentLifeRemaining] = life;
            PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
        }
    }

    public int GetRemainingLives()
    {
        var remainingLives = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentLifeRemaining];
        return remainingLives;
    }


}
