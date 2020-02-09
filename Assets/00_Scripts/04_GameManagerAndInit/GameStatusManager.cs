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
    public static GameStates Status = GameStates.GameRuns;

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
