using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviourPun

{
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

    // Speed Up power up effect
    
    /* 
    This references the 'walkSpeed' float in the 'Player' class making it x2 faster for 6 seconds.  
    The code also instantiates this across the network.
    */ 

    IEnumerator Pickup(Collider player)
    {
        Player playerstats = player.GetComponent<Player>();
        playerstats.walkSpeed = 5.0f;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerstats.walkSpeed = 2.5f;

        PhotonView.Get(gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
    }
}
