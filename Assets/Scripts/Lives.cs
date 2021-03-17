using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{

    [SerializeField]
    private Text livesText;

    public static int life  = 5;

    // Update is called once per frame
    private void Update()
    {
        livesText.text = "LIVES: " + life;
    }
}
