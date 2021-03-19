using Photon.Pun;

public class ToiletRoll : MonoBehaviourPun
{
    

    [PunRPC]
    public void Destroy()
    {
        Destroy(gameObject);
    }


}
