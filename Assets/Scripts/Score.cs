using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int score;

    private void Start()
    {
        score = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentScore];
    }

    // Update is called once per frame
    private void Update()
    {
        // if (player.IsMine)
        scoreText.text = "SCORE: " + GetCurrentScore();
    }

    public void Increment(int amount = 2)
    {

        if (SceneManager.GetActiveScene().buildIndex == 10){
            amount = 5;
        }else{
            amount = 2;
        }
        
        score += amount;

        PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentScore] = score;
        PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);

    }

    public int GetCurrentScore()
    {
        var currentScore = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.PlayerCurrentScore];
        return currentScore;
    }

}
