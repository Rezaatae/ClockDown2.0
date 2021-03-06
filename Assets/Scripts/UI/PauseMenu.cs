using UnityEngine;
using Gravitons.UI.Modal;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI;


    // Update is called once per frame
    private void Update()
    {
        // calling Pause/Resume method when esc key is down
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // sets timescale back to normal
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freezes the game
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        ModalManager.Show(null, "Are you sure you want to quit now?", new[] { new ModalButton() { Text = "YES", Callback = Quit }, new ModalButton() { Text = "NO" } });
    }

    private void Quit()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }
}
