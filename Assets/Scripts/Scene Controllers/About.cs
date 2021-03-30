using UnityEngine.SceneManagement;
using UnityEngine;

public class About : MonoBehaviour
{
    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }
}
