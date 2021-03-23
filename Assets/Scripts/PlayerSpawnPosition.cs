using UnityEngine;
using Photon.Pun;

public class PlayerSpawnPosition : MonoBehaviour
{

    // [SerializeField]
    // private GameObject player;

    private void Start()
    {
        PhotonNetwork.Instantiate(Constants.Prefabs.Player, new Vector3(Random.Range(transform.position.x, transform.position.x + 5), transform.position.y, transform.position.z), Quaternion.identity);
    }

}