using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviourPun
{
    public float duration = 10.0f;
     

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
        playerstats.invincible = true;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerstats.invincible = false;

        PhotonView.Get(gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
    }
}
