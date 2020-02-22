using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Doozy.Engine;
using System;
using Doozy.Engine.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelSaveLoadController : MonoBehaviour
{
    private AsyncOperation nextScene;

    private void Start()
    {
        //Always unlock Level01
        SetLockLevel("Level01", false);
    }

    private void OnEnable()
    {
        Message.AddListener<GameEventMessage>(OnGameStateChanged);
    }

    private void OnDisable()
    {
        Message.RemoveListener<GameEventMessage>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameEventMessage message)
    {
        if (message.EventName == GameStates.GameWon.ToString())
        {
            UnlockNextLevel();
        }
    }

    private void UnlockNextLevel()
    {
        int nextSceneIdx = SceneManager.GetActiveScene().buildIndex + 1;

        // Do need to load the scene in Background, otherwise it is not possible to
        // get the name of an unloaded scene
        nextScene = SceneManager.LoadSceneAsync(nextSceneIdx);
        nextScene.allowSceneActivation = false;
               
        SetLockLevel(SceneManager.GetSceneByBuildIndex(nextSceneIdx).name, false);
    }

    public static bool IsLevelLocked(string levelName)
    {
        int _isLevelLocked = PlayerPrefs.GetInt(levelName + "IsLocked", 1);

        if (_isLevelLocked == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int GetStarsAchieved(string levelName)
    {
        return PlayerPrefs.GetInt(levelName + "Stars", 0);
    }

    public static void SetLockLevel(string levelName, bool isLocked)
    {
        int _isLocked;
        if (isLocked)
        {
            _isLocked = 1;
        }
        else
        {
            _isLocked = 0;
        }

        PlayerPrefs.SetInt(levelName + "IsLocked", _isLocked);
        PlayerPrefs.Save();
    }

    public static void SetStars(string levelName, int stars)
    {
        PlayerPrefs.SetInt(levelName + "Stars", stars);
        PlayerPrefs.Save();
    }

    // Testing
    [TitleGroup("Testing")]
    [SerializeField]
    private string levelName;
    [SerializeField]
    private bool doLock = false;
    [SerializeField]
    private int stars = 0;
    [ResponsiveButtonGroup]
    private void SetLockLevel()
    {
        SetLockLevel(levelName, doLock);
    }
    [ResponsiveButtonGroup]
    private void SetStars()
    {
        SetStars(levelName, stars);
    }
    [ResponsiveButtonGroup]
    private void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
