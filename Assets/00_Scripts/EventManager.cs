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
        GameStatusManager.SetStatus(GameStates.GamePaused, this);
    }

    public void UnPauseGame()
    {
        GameStatusManager.SetStatus(GameStates.GameRuns, this);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
