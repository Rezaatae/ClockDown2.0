using UnityEngine.SceneManagement;
using UnityEngine;

public class AboutController : MonoBehaviour
{
    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene(Constants.Scenes.MainMenu);
    }
}
