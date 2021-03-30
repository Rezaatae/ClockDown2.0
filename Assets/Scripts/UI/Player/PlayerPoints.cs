using UnityEngine;
using Photon.Pun;
using TMPro;
using ExitGames.Client.Photon;

public class PlayerPoints : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private TextMeshProUGUI text;

    public Photon.Realtime.Player Player { get; private set; }

    private void Start()
    {
        text.color = Color.black;
    }
    
    public void SetPointsFor(Photon.Realtime.Player player)
    {
        Player = player;
        SetPlayerPointsText(player);
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == Player)
            SetPlayerPointsText(targetPlayer);
    }

    private void SetPlayerPointsText(Photon.Realtime.Player player)
    {
        var points = (int) player.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore];
        text.text = player.NickName + ", " + points + " points";
    }

}
