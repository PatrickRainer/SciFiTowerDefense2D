using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Doozy.Engine.UI;
using Doozy.Engine;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField, SceneObjectsOnly]
    private TextMeshProUGUI coinsLabel;
    [SerializeField, SceneObjectsOnly]
    private TextMeshProUGUI healthLabel;
    [SerializeField, SceneObjectsOnly]
    private TextMeshProUGUI waveLabel;
    [SerializeField]
    private UIView gameOverPanel;
    [SerializeField]
    private UIView gameWonPanel;
    [SerializeField]
    private UIView gameHUD;
    [SerializeField, Required]
    private UIView pausePanel;

    private void Start()
    {

    }

    private void Update()
    {
        if (GameStatusManager.Status == GameStates.GameLost)
        {
            gameOverPanel.Show();
            gameHUD.Hide();
        }

        if (GameStatusManager.Status == GameStates.GameWon)
        {
            gameWonPanel.Show();
            gameHUD.Hide();
        }

        if (GameStatusManager.Status == GameStates.GamePaused)
        {
            pausePanel.Show();
            gameHUD.Hide();
        }

        if (GameStatusManager.Status == GameStates.GameRuns)
        {
            gameHUD.Show();
            gameOverPanel.Hide();
            gameWonPanel.Hide();
            pausePanel.Hide();
        }
    }

    //private void HidePausePanel()
    //{
    //    pausePanel.SetActive(false);
    //}

    //private void ShowPausePanel()
    //{
    //    pausePanel.SetActive(true);
    //    HideGameHUD();
    //}

    //private void ShowGameOverPanel()
    //{
    //    gameOverPanel.SetActive(true);
    //    HideGameHUD();
    //}

    //private void HideGameOverPanel()
    //{
    //    gameOverPanel.SetActive(true);
    //}

    //private void ShowGameWonPanel()
    //{
    //    gameWonPanel.SetActive(true);
    //    HideGameHUD();
    //}

    //private void HideGameHUD()
    //{
    //    gameHUD.SetActive(false);
    //}

    //private void ShowGameHUD()
    //{
    //    gameHUD.SetActive(true);
    //}

    private void OnGUI()
    {
        ShowCoins();
        ShowHealth();
        ShowWave();
    }

    private void ShowCoins()
    {
        coinsLabel.text = "Coins: " + LevelManager.GetCoins().ToString();
    }

    private void ShowHealth()
    {
        healthLabel.text = "Health: "+ LevelManager.PlayerHealth.ToString();
    }

    private void ShowWave()
    {
        waveLabel.text = "Wave: " + LevelManager.GetWave().ToString();
    }
   
}
