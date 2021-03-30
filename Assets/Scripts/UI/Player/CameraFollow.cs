using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offset;

    void FixedUpdate()
    {

        if (GameObject.Find(Constants.Prefabs.PlayerPhotonClone) != null)
        {
            target = GameObject.Find(Constants.Prefabs.PlayerPhotonClone).GetComponent<Transform>();
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
            transform.position = desiredPosition;
            transform.LookAt(target);
        }
        
        
    }

}
