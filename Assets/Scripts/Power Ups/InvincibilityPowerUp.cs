using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviourPun
{
    public float duration = 10.0f;
     
// Needed for player who is untagged to register colliding with gameObject
// Initiates PickUp effect coroutine
    [PunRPC]
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Untagged"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    // Invincibility power up effect
    
    /* 
    This references the 'invincible' boolean in 'Player' class making it true. This turns off hit
    detection for player against enemies for 10 seconds. The code also adds one life to 'Lives' and
    instantiates this across the network.
    */ 
     
    IEnumerator Pickup(Collider player)
    {
        Player playerstats = player.GetComponent<Player>();
        playerstats.invincible = true;
        
        if (photonView.IsMine)
        {
            FindObjectOfType<Lives>().Increase();
        }

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerstats.invincible = false;

        PhotonView.Get(gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
    }
}
