using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{

    [SerializeField]
    private float velocity = 20f;
    [SerializeField]
    private float lifeTime = 1f;
    
    private int firedbyLayer;
    private float lifeTimer;

    private void Update()
    {
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
        
        if (collider.gameObject.layer == 12)
        {   
            FindObjectOfType<Player>().IncrementJumpToken();
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
        lifeTimer = Time.time;
        transform.position = position;
        transform.eulerAngles = euler;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 vop = Vector3.ProjectOnPlane(transform.forward, Vector3.forward);
        transform.forward = vop;
        transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);

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
