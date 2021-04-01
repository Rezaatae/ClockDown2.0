using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviourPun
{
    // waypoints stored here in transforms
    [SerializeField]
    private Transform[] positions;

    private Transform nextPos;

    public int speed;

    private int nextPosIndex;

    [SerializeField]
    private GameObject effectA;

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
        // the enemy object must always transform to next position index
        // if the enemy object is at the "next" positon, next position index must increase
        
        if (transform.position == nextPos.position)
        {
            nextPosIndex++;
            // if the index of the next position resembles the size of the positions, next position is set back to the first position
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
        Destroy(gameObject);
    }


    // method for instantiating the explosion effect when enemy dies
    private IEnumerator CreateEffect(GameObject prefab, Vector3 position)
	{
		GameObject go = PhotonNetwork.Instantiate(prefab.name, position, Quaternion.identity);
		
        yield return new WaitForSeconds(explosionLifeTime);
		PhotonNetwork.Destroy(PhotonView.Get(go));
	}

    

    
}
