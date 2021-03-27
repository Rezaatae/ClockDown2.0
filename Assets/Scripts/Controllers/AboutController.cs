using UnityEngine.SceneManagement;
using UnityEngine;

public class AboutController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }
}
