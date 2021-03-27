using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AboutController : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.MainMenu);
    }
}
