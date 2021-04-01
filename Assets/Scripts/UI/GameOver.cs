using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void QuitGame()
    {
        
        SceneManager.LoadScene(2); // main menu to be loaded on game over
    }
}
