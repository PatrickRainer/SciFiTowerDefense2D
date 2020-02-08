using Doozy.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum GameStates { GameLost, GameWon, GamePaused, GameRuns }

public class GameStatusManager : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    public static GameStates Status = GameStates.GameRuns;

    private void Update()
    {
        if (Status == GameStates.GameLost || Status == GameStates.GameWon || Status == GameStates.GamePaused)
        {
            SetTimeScalePausing();
        }
        else if (Status == GameStates.GameRuns)
        {
            SetTimeScaleRunning();
        }
    }

    private void SetTimeScaleRunning()
    {
        Time.timeScale = 1;
        //Debug.Log(isGamePaused);
    }

    private void SetTimeScalePausing()
    {
        Time.timeScale = 0;
        //Debug.Log(isGamePaused);
    }

}
