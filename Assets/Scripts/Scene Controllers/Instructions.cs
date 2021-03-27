using UnityEngine.SceneManagement;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }
}
