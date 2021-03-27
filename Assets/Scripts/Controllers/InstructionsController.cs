using UnityEngine.SceneManagement;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }
}
