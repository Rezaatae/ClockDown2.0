using UnityEngine;
using TMPro;
using Photon.Pun;


public class PlayerTag : MonoBehaviour
{

    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private TextMeshProUGUI playerNameText;

    [Tooltip("Pixel offset from the player target")]
    [SerializeField]
    private Vector3 screenOffset = new Vector3(0f,30f,0f);

    private float characterControllerHeight = 1f;
    private Transform targetTransform;
    private Renderer targetRenderer;
    private Vector3 targetPosition;

    private Player target;

    private void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Update()
    {
        if (target == null)
        {
            PhotonNetwork.Destroy(this.gameObject);
            
            return;
        }
    }

    private void LateUpdate()
    {
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
        }
    }

    public void SetTarget(Player target)
    {
        if (target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        this.target = target;
        targetTransform = this.target.GetComponent<Transform>();
        targetRenderer = this.target.GetComponent<Renderer>();
        CharacterController characterController = target.GetComponent<CharacterController>();
        // Get data from the Player that won't change during the lifetime of this Component
        if (characterController != null)
        {
            characterControllerHeight = characterController.height;
        }   
        if (playerNameText != null)
        {
            playerNameText.text = this.target.photonView.Owner.NickName;
        }
    }

}