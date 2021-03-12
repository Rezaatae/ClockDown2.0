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

    private ArrayList playerIds = new ArrayList();

    bool isMoving;

    void Start()
    {
        GetPlayers();
    }

    void GetPlayers()
    {
        foreach(var player in PhotonNetwork.PlayerList)
        {
            int playedId = System.Convert.ToInt32(player.CustomProperties[Constants.PlayerId]);
            playerIds.Add(playedId);
        }

        Debug.Log(playerIds.Count);
    }

    void Update()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(Constants.WhosTurnIndex) && PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("player_id"))
        {
            whosTurn = System.Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[Constants.WhosTurnIndex]);
            playedId = System.Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerId]);
            Debug.Log("Player " + playerIds[whosTurn] + "'s turn");
            Debug.Log("Local player " + playedId);
            if (player.IsMine && (int) playerIds[whosTurn] == playedId)
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
        if (whosTurn == PhotonNetwork.CurrentRoom.PlayerCount - 1)
            whosTurn = 0;
            else 
                whosTurn++;

        PhotonNetwork.CurrentRoom.CustomProperties[Constants.WhosTurnIndex] = whosTurn;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
    }
}
