using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedPowerUp : PowerUps, IPlayerEvent
{
    public float speedMultiplier = 3.0f;

    protected override void PowerUpPayload()       
    {
        base.PowerUpPayload();
        player.SpeedBoostOn(speedMultiplier);
    }

    protected override void PowerUpExpired()       
    {
        player.SpeedBoostOff();
        base.PowerUpExpired();
    }

    void IPlayerEvent.OnPlayerHurt (int newLives)
    {
        if (powerUpState != PowerUpState.IsCollected)
        {
            return;
        }

        PowerUpExpired();
    }

    void IPlayerEvent.OnPlayerReachExit (GameObject exit)
    {

    }

}