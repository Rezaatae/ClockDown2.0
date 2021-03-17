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
        player = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        
        if (player.IsMine)
        {
        target = GameObject.Find("Guy").GetComponent<Transform>();
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
        
        transform.LookAt(target);
        }
        
    }

}
