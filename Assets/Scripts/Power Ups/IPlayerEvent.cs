using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public interface IPlayerEvent : IEventSystemHandler
{
    void OnPlayerHurt (int newLives);

    void OnPlayerReachExit (GameObject exit);
}

