using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Leaderboard : MonoBehaviour
{

    // public static Leaderboard Instance;

    // [SerializeField]
	private GameObject particleEmitter;

    [SerializeField]
    private Button backToGameLobbyButton;

	// [SerializeField]
	private ParticleSystem effectA;

	// [SerializeField]
	private ParticleSystem effectB;

	void Start()
    {
		/// -----------------------------------------
		/// Instanciate into a box of 5 x 5 x 5 (xyz)
		/// -----------------------------------------
        // Explosion(particleEmitter.transform.position);
        if (PhotonNetwork.IsMasterClient)
            backToGameLobbyButton.gameObject.SetActive(true);
        else
            backToGameLobbyButton.gameObject.SetActive(false);

        

	}

    public void BackToGameLobby()
    {
        PhotonNetwork.LoadLevel(Constants.Scenes.Game.Levels.GameLobby);
    }

    public void LeaveGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(Constants.Scenes.Game.MainMenu);
        else
            SceneManager.LoadScene(Constants.Scenes.Game.MainMenu);
        PhotonNetwork.LeaveRoom();
    }

	/// -----------------------------------------
	/// Create an explosion at the given location
	/// -----------------------------------------
	public void Explosion(Vector3 position)
	{
		instantiate(effectA, position);
		instantiate(effectB, position);
	}

	/// -----------------------------------------
	/// Instantiate a Particle system from prefab
	/// -----------------------------------------
	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate(prefab,position,Quaternion.identity) as ParticleSystem;

		/// -----------------------------
		// Make sure it will be destroyed
		/// -----------------------------
		Destroy(
			newParticleSystem.gameObject,
			0.5f
		);

		return newParticleSystem;
	}

}
