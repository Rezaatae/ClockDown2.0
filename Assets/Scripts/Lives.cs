using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public Text livesText;

    public static int life  = 5;


    // Update is called once per frame
    void Update()
    {
        livesText.text = "LIVES: " + life;
    }
}
