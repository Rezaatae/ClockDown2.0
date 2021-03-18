using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPun
{

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private float secondsLeft;

    [SerializeField]
    private bool takingAway;

    [SerializeField]
    private GameManager gameManager;

    // Update is called once per frame
    private void Update()
    {
            
        if (PhotonNetwork.IsMasterClient)
            GetComponent<PhotonView>().RPC("UpdateTimer", RpcTarget.AllBuffered);
        
    }

    [PunRPC]
    public void UpdateTimer()
    {
        secondsLeft -= Time.deltaTime;
        timerText.text = secondsLeft.ToString("f2");
        if(secondsLeft <= 0)
        {
            gameManager.CompleteLevel();
        }
    }


}
