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
        secondsLeft -= Time.deltaTime;
        timerText.text = secondsLeft.ToString("f2");
        if(secondsLeft <= 0)
        {
            gameManager.CompleteLevel();
        }     
        
    }

}
