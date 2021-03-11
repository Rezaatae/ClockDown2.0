using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Stone : MonoBehaviour
{

    [SerializeField]
    private Route currentPos;

    [SerializeField]
    private PhotonView player;

    int routePos;

    public int steps;

    private int whosTurn;

    private int playedId;

    bool isMoving;

    void Start()
    {
        // Hashtable playerProps = new Hashtable();
        // playerProps["player_id"] = PhotonNetwork.CurrentRoom.PlayerCount;
        // PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);
    }

    void Update()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("whos_turn") && PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("player_id"))
        {
            whosTurn = (int) PhotonNetwork.CurrentRoom.CustomProperties["whos_turn"];
            playedId = (int) PhotonNetwork.LocalPlayer.CustomProperties["player_id"];
            Debug.Log("Player " + whosTurn + "'s turn");
            Debug.Log("Local player " + playedId);
            if (player.IsMine && whosTurn == playedId)
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
        } else
        {
            Debug.Log("Nope");
        }
        
    }



    IEnumerator Move()
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
        UpdateWhosTurn();
    }
    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }

    private void UpdateWhosTurn()
    {
        if (whosTurn >= PhotonNetwork.CurrentRoom.PlayerCount)
        {
            whosTurn = 1;
        } else {
            whosTurn++;
        }

        PhotonNetwork.CurrentRoom.CustomProperties["whos_turn"] = whosTurn;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
    }
}
