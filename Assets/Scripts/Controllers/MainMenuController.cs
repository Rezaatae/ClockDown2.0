using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void OnClickCreateRoom()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.Menu.NewGame);
    }

    public void OnClickJoinRoom()
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

}
