using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void OnClickCreateRoom()
    {
        SceneManager.LoadScene(Constants.Scenes.CreateGame);
    }

    public void OnClickJoinRoom()
    {
        SceneManager.LoadScene(Constants.Scenes.JoinGame);
    }

}
