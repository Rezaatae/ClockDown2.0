using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public float velocity = 20f;
    public float lifeTime = 1f;
    private int firedbyLayer;
    private float lifeTimer;

    private Score currentPlayerScore;

    public void SetCurrentPlayerScore(Score score)
    {
        currentPlayerScore = score;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeOut());
    }

    // Update is called once per frame
    void Update()
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
        // do something with the object that was hit (collider) e.g. collider.gameObject
        
        if (collider.gameObject.layer == 12)
        {
            if (photonView.IsMine)
                currentPlayerScore.Increment();
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
    
    }

    public void Fire(Vector3 position, Vector3 euler, int layer)
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

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(lifeTime);
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Destroy()
    {
        Destroy(this.gameObject);
    }


}
