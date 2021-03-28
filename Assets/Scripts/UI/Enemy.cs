using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviourPun
{

    [SerializeField]
    private Transform[] positions;

    private Transform nextPos;

    public int speed;

    private int nextPosIndex;

    private float dist;

    [SerializeField]
    private GameObject effectA;

    // [SerializeField]
    // private GameObject effectB;

    [SerializeField]
    private float explosionLifeTime = 1f;

    private void Start()
    {
        nextPos = positions[0];
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == nextPos.position)
        {
            nextPosIndex++;
            if (nextPosIndex >= positions.Length)
            {
                nextPosIndex = 0;
            }
            nextPos = positions[nextPosIndex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed*Time.deltaTime);
        }

    }

    [PunRPC]
    public void Destroy()
    {
        StartCoroutine(CreateEffect(effectA, transform.position));
        // CreateEffect(effectB, transform.position);
        Destroy(gameObject);
    }


    private IEnumerator CreateEffect(GameObject prefab, Vector3 position)
	{
		GameObject go = PhotonNetwork.Instantiate(prefab.name, position, Quaternion.identity);
		
        yield return new WaitForSeconds(explosionLifeTime);
		PhotonNetwork.Destroy(PhotonView.Get(go));
	}

    

    
}
