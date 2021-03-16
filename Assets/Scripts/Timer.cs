using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int secondsLeft = 30;

    public bool takingAway = false;

    // Start is called before the first frame update
    void Start()
    {
        timerText.GetComponent<Text>().text = "00:" + secondsLeft;

    }

    // Update is called once per frame
    void Update()
    {
        if(takingAway == false && secondsLeft > 0){
            StartCoroutine(ReduceTime());
        }
    }

    IEnumerator ReduceTime(){
        takingAway = true;
        yield return new WaitForSeconds(1f);
        secondsLeft --;
        timerText.GetComponent<Text>().text = "00:" + secondsLeft;
        takingAway = false;
    }
}
