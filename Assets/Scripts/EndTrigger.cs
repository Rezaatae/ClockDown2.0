using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            gameManager.CompleteLevel();
        }
    }
}
