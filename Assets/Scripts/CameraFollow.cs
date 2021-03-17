using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private PhotonView player;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        Debug.Log("Player is mine: " + player.IsMine);
        target = GameObject.Find("Guy").GetComponent<Transform>();
    
    }

    void FixedUpdate()
    {
        
        if (player.IsMine)
        {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
        
        transform.LookAt(target);
        }
        
    }

}
