using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using Sirenix.OdinInspector;

public class EventManager : MonoBehaviour
{

    //public static void RaiseGameOverEvent()
    //{
    //    GameEventMessage.SendEvent("GameOver");
    //}

    //public static void RaiseGameWonEvent()
    //{
    //    GameEventMessage.SendEvent("GameWon");
    //}

    //public static void RaisePauseGame()
    //{
    //    GameEventMessage.SendEvent("PauseGame");
    //}

    //public static void RaiseUnPauseGame()
    //{
    //    GameEventMessage.SendEvent("UnPauseGame");
    //}

    public static void RaiseEnemyDestroyed()
    {
        GameEventMessage.SendEvent("EnemyDestroyed");
    }



}
