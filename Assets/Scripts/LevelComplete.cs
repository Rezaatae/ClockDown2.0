using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void LoadNextLevel()
    {
        //TODO: use photon instead
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
