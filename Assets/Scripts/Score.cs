using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Score : MonoBehaviour
{

    [SerializeField]
    private Text scoreText;

    private int score = 0;

    public void Start()
    {
        // Hashtable dict = new Hashtable();
        // dict[Constants.PlayerCurrentScore] = score;
        // PhotonNetwork.SetPlayerCustomProperties(dict);
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = "SCORE: " + GetCurrentScore();
    }

    public void Increment(int amount = 5)
    {
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
