using Doozy.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    private static bool isGamePaused = false;

    [ShowInInspector]
    public static bool IsGamePaused { get => isGamePaused;}

    private void OnEnable()
    {
        Message.AddListener<GameEventMessage>("PauseGame", OnPauseGame);
        Message.AddListener<GameEventMessage>("UnPauseGame", OnUnPauseGame);
    }
    
    private void OnDisable()
    {
        Message.RemoveListener<GameEventMessage>("PauseGame", OnPauseGame);
        Message.RemoveListener<GameEventMessage>("UnPauseGame", OnUnPauseGame);
    }

    private void OnUnPauseGame(GameEventMessage obj)
    {
        Time.timeScale = 1;
        isGamePaused = false;
        Debug.Log(isGamePaused);
    }

    private void OnPauseGame(GameEventMessage obj)
    {
        isGamePaused = true;
        Time.timeScale = 0;
        Debug.Log(isGamePaused);
    }

}
