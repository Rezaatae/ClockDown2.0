using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviour
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
        secondsLeft -= Time.deltaTime;
        timerText.text = secondsLeft.ToString("f2");
        if(secondsLeft <= 0)
        {
            gameManager.CompleteLevel();
        }     
        
    }
    
}
