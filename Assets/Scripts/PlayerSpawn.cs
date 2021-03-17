using UnityEngine;
using Photon.Pun;

public class PlayerSpawn : MonoBehaviour
{

    private void Start()
    {
        PhotonNetwork.Instantiate(Constants.Prefabs.Guy, new Vector3(Random.Range(-4, 0), 0, 0), Quaternion.identity);
    }

}