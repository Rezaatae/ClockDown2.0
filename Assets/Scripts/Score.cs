using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField]
    private Text scoreText;

    public static int score = 0;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + score;
    }

}
