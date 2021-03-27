using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene(Constants.Scenes.MainMenu);
    }
}
