using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviourPun

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
        playerstats.walkSpeed *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerstats.walkSpeed /= multiplier;

        PhotonView.Get(gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
    }
}
