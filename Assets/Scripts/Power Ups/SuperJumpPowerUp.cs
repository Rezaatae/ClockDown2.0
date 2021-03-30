using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SuperJumpPowerUp : MonoBehaviourPun
{
    public float multiplier = 2.0f;
    public float duration = 6.0f;

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

    // Super Jump power up effect
    
    /* 
    This references the 'jumpHeight' float in the 'Player' class making it x2 more powerful for 6 seconds.  
    The code also instantiates this across the network.
    */ 

    IEnumerator Pickup(Collider player)
    {
        Player playerstats = player.GetComponent<Player>();
        playerstats.jumpHeight *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerstats.jumpHeight /= multiplier;

        PhotonView.Get(gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
    }
}
