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
            
                playerPoints.SetPointsFor(player);
            }
            
        }
    }

    private int ByPlayerPoints(Photon.Realtime.Player p1, Photon.Realtime.Player p2)
    {
        return ((int) p1.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore]).CompareTo((int) p2.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore]);
    }

}