using UnityEngine.EventSystems;
using UnityEngine;
using Photon.Pun;

public class PowerUps : MonoBehaviourPun
{
    public string powerUpName;
    public bool expiresImmediately;
    protected Player player; 

    protected enum PowerUpState
    {
        IsCollected,
        IsExpiring
    }

    protected PowerUpState powerUpState;

    [PunRPC]
    protected virtual void OnTriggerEnter(Collider other)
        {
            PowerUpCollected(other.gameObject);
        }
  
    
    protected virtual void PowerUpCollected (GameObject gameObjectCollectingPowerUp)
    {
        if (gameObjectCollectingPowerUp.tag != "Player")
        {
            return;
        }

        if (powerUpState == PowerUpState.IsCollected || powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsCollected;

        player = gameObjectCollectingPowerUp.GetComponent<Player> ();

        PowerUpPayload ();

        foreach (GameObject go in Listeners.main.listeners)
        {
            ExecuteEvents.Execute<IPowerUpEvent> (go, null, (x,y) => x.OnPowerUpCollected (this, player));
        }
    }

    protected virtual void PowerUpPayload()
    {
        Debug.Log ("Power Up collected, issuing payload for: " + gameObject.name);

        if (expiresImmediately)
        {
            PowerUpExpired();
        }
    }

    protected virtual void PowerUpExpired()
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsExpiring;

        foreach (GameObject go in Listeners.main.listeners)
        {
            ExecuteEvents.Execute<IPowerUpEvent> (go, null, (x, y) => x.OnPowerUpExpired (this, player));
        }
        Debug.Log ("Power Up has expired, removing after a delay for: " + gameObject.name);
        Destroy();
    }

    [PunRPC]
    public void Destroy()
    {
        Destroy(gameObject);
    }
}

