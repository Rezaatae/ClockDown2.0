using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    // bullet velocity
    [SerializeField]
    private float velocity = 20f;
    // bullet lifetime
    [SerializeField]
    private float lifeTime = 1f;
    
    // layer respinsible for projecting the bullet
    private int firedbyLayer;

    
    private float lifeTimer;

    private void Update()
    {
        
        /* 
        using a raycast to calculate the next frame position of the bullet and trigger a hit if the position in the next frame 
        collides with an object (of layer other that the firedbylayer layer)
        */
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, velocity * Time.deltaTime, ~(1 << firedbyLayer))){
            transform.position = hit.point;
            Vector3 reflected = Vector3.Reflect(transform.forward, hit.normal);
            Vector3 direction = transform.forward;
            Vector3 vop = Vector3.ProjectOnPlane(reflected, Vector3.forward);
            transform.forward = vop;
            transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);
            Hit(transform.position, direction, reflected, hit.collider);
        } else
        {
            transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        }
    }
    
    private void Hit(Vector3 position, Vector3 direction, Vector3 reflected, Collider collider)
    {
        // here we control the events when a collision is detected
        if (collider.gameObject.layer == 12)
        {   
            PhotonView.Get(collider.gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
            StartCoroutine(Expire(0));
        }
        else
        {
            StartCoroutine(Expire(lifeTime));
        }        
    
    }

    public void SetTrajectory(Vector3 position, Vector3 euler, int layer)
    {

        // this enforces the bullets' trajectory in one plane

        lifeTimer = Time.time; // time-stamp
        transform.position = position; // position projected from
        transform.eulerAngles = euler; // the euler angle of the trajectory
        transform.position = new Vector3(transform.position.x, transform.position.y, 0); // supressing the movement is x and y axis
        Vector3 vop = Vector3.ProjectOnPlane(transform.forward, Vector3.forward); // calculating the projection of the bullet
        transform.forward = vop;
        transform.rotation = Quaternion.LookRotation(vop, Vector3.forward); // making bullet face the target when projected

    }

    private IEnumerator Expire(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PhotonView.Get(gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Destroy()
    {
        Destroy(gameObject);
    }


}
