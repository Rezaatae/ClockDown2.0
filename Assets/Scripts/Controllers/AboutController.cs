﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AboutController : MonoBehaviour
{
    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene(Constants.Scenes.MainMenu);
    }
}
