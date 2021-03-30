using Photon.Pun;

public class Potion : MonoBehaviourPun
{
    

    [PunRPC]
    public void Destroy()
    {
        Destroy(gameObject);
    }

}