using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedPowerUp : PowerUps
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
}