using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Stone : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Route currentPos;

    // [SerializeField]
    // private PhotonView player;

    private GameManager gameManager;

    private int routePos;

    public int steps;

    // private int whosTurnIndex;

    // private ArrayList playerIds = new ArrayList();

    private bool isMoving;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        // GetPlayers();
        // gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        // if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(Constants.WhosTurnIndex) && PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("player_id"))
        // {
        //     whosTurnIndex = System.Convert.ToInt32(PhotonNetwork.CurrentRoom.CustomProperties[Constants.WhosTurnIndex]);
        //     int playedId = System.Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerId]);
        //     if (player.IsMine && (int) playerIds[whosTurnIndex] == playedId)
        //     {
            if (gameManager.IsLocalPlayersTurn()) 
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
                   
            // }
        // }
        
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
        // UpdateWhosTurn();
    }
    private bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }

    // public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    // {
    //     GetPlayers();
    // }

    // private void UpdateWhosTurn()
    // {
    //     if (whosTurnIndex == PhotonNetwork.CurrentRoom.PlayerCount - 1)
    //         whosTurnIndex = 0;
    //         else 
    //             whosTurnIndex++;

    //     PhotonNetwork.CurrentRoom.CustomProperties[Constants.WhosTurnIndex] = whosTurnIndex;
    //     PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
    // }

    // private void GetPlayers()
    // {
    //     foreach(var player in PhotonNetwork.PlayerList)
    //     {
    //         int playedId = System.Convert.ToInt32(player.CustomProperties[Constants.PlayerId]);
    //         playerIds.Add(playedId);
    //     }

    //     Debug.Log(playerIds.Count);
    // }
}
