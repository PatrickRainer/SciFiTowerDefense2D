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
            PauseGame();
        }
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1;
        //Debug.Log(isGamePaused);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //Debug.Log(isGamePaused);
    }

}
