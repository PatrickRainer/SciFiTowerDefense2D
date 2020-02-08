using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static void RaiseEnemyDestroyed()
    {
        GameEventMessage.SendEvent("EnemyDestroyed");
    }

    public void PauseGame()
    {
        GameStatusManager.Status = GameStates.GamePaused;
    }

    public void UnPauseGame()
    {
        GameStatusManager.Status = GameStates.GameRuns;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
