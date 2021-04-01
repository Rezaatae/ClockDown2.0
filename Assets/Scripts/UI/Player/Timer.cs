using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private float secondsLeft;

    [SerializeField]
    private bool takingAway;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    private void Update()
    {
        // decreasing the timer UI per frame
        secondsLeft -= Time.deltaTime;
        timerText.text = secondsLeft.ToString("f2");
        if(secondsLeft <= 0)
        {
            gameManager.CompleteLevel(); // complete level when time runs out (load next level)
        }     
        
    }

}
