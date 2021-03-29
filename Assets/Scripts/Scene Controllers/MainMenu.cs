using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void OnClickNewGame()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.Menu.NewGame);
    }

    public void OnClickJoinGame()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.Menu.JoinGame);
    }

    public void OnClickAbout()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.Menu.About);
    }

    public void OnClickInstructions()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.Menu.Instructions);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

}
