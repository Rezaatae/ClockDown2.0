using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;

public class Timer : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private float secondsLeft;

    [SerializeField]
    private bool takingAway;

    [SerializeField]
    private GameManager gameManager;

    private Hashtable dict = new Hashtable();


    private void Start()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(dict);
    }

    // Update is called once per frame
    private void Update()
    {
            
        if (PhotonNetwork.IsMasterClient)
        {
            secondsLeft -= Time.deltaTime;
            dict.Add("current_time_left", secondsLeft);
            PhotonNetwork.CurrentRoom.SetCustomProperties(dict);
        }
        //     GetComponent<PhotonView>().RPC("UpdateTimer", RpcTarget.AllBuffered);
            
        
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        object currentTimeLeft;
        if (propertiesThatChanged.TryGetValue("current_time_left", out currentTimeLeft))
        {
            timerText.text = secondsLeft.ToString("f2");
            if((int) currentTimeLeft <= 0)
            {
                gameManager.CompleteLevel();
            }
        }
        
    }

    // [PunRPC]
    // public void UpdateTimer()
    // {
    //     secondsLeft -= Time.deltaTime;
        
        
    // }


}
