using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Stone : MonoBehaviour
{

    [SerializeField]
    private Route currentPos;

    [SerializeField]
    private PhotonView player;

    private GameManager gameManager;

    private int routePos;

    public int steps;

    private bool isMoving;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.IsLocalPlayersTurn() && player.IsMine) 
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
            {
                steps = Random.Range(1, 7);
                Debug.Log("Dice Rolled: " + steps);

                if (routePos + steps < currentPos.childNodeList.Count)
                {
                    StartCoroutine(Move());
                }
                else
                {
                Debug.Log("Rolled number too high");
                }
            }
        }
        
    }

    private IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        while(steps > 0)
        {
            Vector3 nextPos = currentPos.childNodeList[routePos + 1].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            steps--;
            routePos++;
        }
        isMoving = false;
        gameManager.UpdateWhosTurn();
    }
    
    private bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }

}
