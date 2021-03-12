﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void OnClick_CreateRoom()
    {
        SceneManager.LoadScene(Constants.Scenes.CreateGame);
    }

    public void OnClick_JoinRoom()
    {
        SceneManager.LoadScene(Constants.Scenes.JoinGame);
    }

}
