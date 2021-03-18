using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lives : MonoBehaviour
{

    [SerializeField]
    private Text livesText;

    private int life;

    private void Start()
    {
        life = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentLifeRemaining];
    }
    

    // Update is called once per frame
    private void Update()
    {
        livesText.text = "LIVES: " + GetRemainingLives();
    }

    public void Deduct()
    {
        life--;

        PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentLifeRemaining] = life;
        PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    
    }

    public int GetRemainingLives()
    {
        var remainingLives = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentLifeRemaining];
        return remainingLives;
    }


}
