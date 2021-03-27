using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class NameSelection : MonoBehaviour
{

    [SerializeField]
    private GameObject inputPanel;

    [SerializeField]
    private TMP_InputField playerNickNameTextField;

    [SerializeField]
    private Button continueButton;

    public void OnClickContinue()
    {
        PhotonNetwork.NickName = playerNickNameTextField.text;
        SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
    }

}