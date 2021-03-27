using UnityEngine.EventSystems;


public interface IPowerUpEvent : IEventSystemHandler
    {
        void OnPowerUpCollected (PowerUps powerup, Player player);

        void OnPowerUpExpired (PowerUps powerup, Player player);
    }


