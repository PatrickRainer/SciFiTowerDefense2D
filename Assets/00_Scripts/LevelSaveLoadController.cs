using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelSaveLoadController : MonoBehaviour
{

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
