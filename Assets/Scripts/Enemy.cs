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

    private void Start()
    {
        nextPos = positions[0];
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
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
    public void DestroyEnemy()
    {
        Destroy(gameObject);
        // StartCoroutine(TimeOut(0));
    }

    

    
}
