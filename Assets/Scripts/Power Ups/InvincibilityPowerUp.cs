using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviourPun
{

    // When Player collides with a red bottle, the red bottle prefab is destroyed
    [PunRPC]
    public void Destroy()
    {
        Destroy(gameObject);
    }

}

