using UnityEngine;
using Photon.Pun;

public class PlayerPointsList : MonoBehaviour
{
    
    [SerializeField]
    private Transform content;

    [SerializeField]
    private PlayerPoints playerPointsPrefab;

    private void Start()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player != PhotonNetwork.LocalPlayer)
            {
                PlayerPoints playerPoints = Instantiate(playerPointsPrefab, content);
                if (playerPoints != null)
                    playerPoints.SetPointsFor(player);
            }
        }
    }

}
