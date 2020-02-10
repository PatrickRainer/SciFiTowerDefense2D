using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    [SerializeField]
    private GameObject levelUI;

    private void Start()
    {
        levelUI.SetActive(true);
        GameStatusManager.SetStatus(GameStates.GameRuns, this);
    }
}
