using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviourPun
{

    [PunRPC]
    public void Destroy()
    {
        Destroy(gameObject);
    }

}

