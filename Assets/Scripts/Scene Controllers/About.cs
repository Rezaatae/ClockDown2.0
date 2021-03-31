using UnityEngine.SceneManagement;
using UnityEngine;

public class About : MonoBehaviour
{
// When the back button is clicked in the about scene, the scene changes to 'Main Menu'
    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }
}
