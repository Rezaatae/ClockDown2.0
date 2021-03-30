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
        score = (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore];
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + GetCurrentScore();
    }

    public void Increment(int amount = 2)
    {

        amount = SceneManager.GetActiveScene().buildIndex == 12 ? 5 : 2;
        
        score += amount;

        PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore] = score;
        PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);

    }

    private int GetCurrentScore()
    {
        return (int) PhotonNetwork.LocalPlayer.CustomProperties[Constants.Scenes.Game.Objects.PlayerCurrentScore];
    }

}
