using Doozy.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum GameStates { GameLost, GameWon, GamePaused, GameRuns }

public class GameStatusManager : MonoBehaviour
{
    [ShowInInspector]
    private static GameStates status = GameStates.GameRuns;
    public static GameStates Status { get => status;}
    public static void SetStatus(GameStates _status, MonoBehaviour sender)
    {
        status = _status;
        //Debug.Log("GameState changed to: " + _status + " by: " + sender.name);

        GameEventMessage.SendEvent(Status.ToString(), sender);
    }


    private void Update()
    {
        if (Status == GameStates.GameLost || Status == GameStates.GameWon || Status == GameStates.GamePaused)
        {
            Time.timeScale = 0;
        }
        else if (Status == GameStates.GameRuns)
        {
            Time.timeScale = 1;
        }
    }
}
