using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool isGamePaused = false;

    public static bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }
}
