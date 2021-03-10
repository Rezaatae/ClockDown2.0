using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    
    [SerializeField]
    private Route currentPos;

    int routePos;

    public int steps;

    bool isMoving;

    void Update()
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



    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        // while(steps > 0)
        // {
        //     Vector3 nextPos = currentPos.childNodeList[routePos + 1].position;
        //     while (MoveToNextNode(nextPos))
        //     {
        //         yield return null;
        //     }
        //     yield return new WaitForSeconds(0.1f);
        //     steps--;
        //     routePos++;
        // }
        isMoving = false;
    }
    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }
}
