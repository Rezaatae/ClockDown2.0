using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public interface IPowerUpEvent : IEventSystemHandler
    {
        void OnPowerUpCollected (PowerUps powerup, Player player);

        void OnPowerUpExpired (PowerUps powerup, Player player);
    }


