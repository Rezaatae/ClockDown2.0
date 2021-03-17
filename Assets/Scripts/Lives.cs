using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Lives : MonoBehaviour
{

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private int life  = 5;

    private PhotonView player;

    public void SetPlayerPhotonView(PhotonView player)
    {
        this.player = player;
    }

    private void Start()
    {
        Hashtable lifeDict = new Hashtable();
        lifeDict[Constants.PlayerCurrentLifeRemaining] = life;
        PhotonNetwork.SetPlayerCustomProperties(lifeDict);
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
