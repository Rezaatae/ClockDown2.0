using Photon.Realtime;

public class LocalizePhotonErrorMessage
{

    public static string disconnectCause(DisconnectCause cause)
    {
        switch (cause)
        {
            default:
            return "There was an issue contacting our servers. Please try again after a few seconds";
        }
    }

    public static string localizedMessage(int errorCode)
    {

        switch (errorCode)
        {
            case Constants.PhotonErrorCodes.GameAlreadyExists:
            return "There is already an active game with this name, try using a different name or joining this one";
            case Constants.PhotonErrorCodes.GameDoesNotExist:
            return "Looks like you're trying to join a game that has either finished or never existed";
            case Constants.PhotonErrorCodes.GameFull:
            return "This game already has the maximum amount of players allowed";
            case Constants.PhotonErrorCodes.GameClosed:
            return "This game has been closed by the creator, please join another room";
            default:
            return "Unimplemented: " + errorCode;
        }

    }

    

}