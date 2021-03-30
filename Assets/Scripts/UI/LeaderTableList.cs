using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class LeaderTableList : MonoBehaviour
{
    
    [SerializeField]
    private Transform content;

    [SerializeField]
    private PlayerPoints playerPointsPrefab;

    private void Start()
    {

        List<Photon.Realtime.Player> playerList = new List<Photon.Realtime.Player>(PhotonNetwork.PlayerList);
        
        playerList.Sort(ByPlayerPoints);
        
        foreach (var player in playerList)
        {
            PlayerPoints playerPoints = Instantiate(playerPointsPrefab, content);

            if (playerPoints != null)
            {
                if (playerList[0].Equals(player))
                    playerPoints.color = Color.yellow;
                else
                    playerPoints.color = Color.black;
            
                playerPoints.SetPointsFor(player);
            }
            
        }
    }

    private int ByPlayerPoints(Photon.Realtime.Player p1, Photon.Realtime.Player p2)
    {
        var player1Points = ((int) p1.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore]);
        var player2Points =  ((int) p2.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore]);

        return player1Points < player2Points ? player1Points : player2Points;
    }

}