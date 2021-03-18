using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private Text timerText;

    public float secondsLeft = 60f;

    public bool takingAway = false;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsLeft -= Time.deltaTime;
        timerText.text = secondsLeft.ToString("f2");
        if(secondsLeft <= 0)
        {
            gameManager.CompleteLevel();
        }     
        
    }
}
