using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SuperJumpPowerUp : MonoBehaviourPun
{
    public float multiplier = 2.0f;
    public float duration = 6.0f;

    
    [PunRPC]
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Untagged"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    
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
