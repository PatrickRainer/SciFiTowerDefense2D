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
    [SerializeField, SceneObjectsOnly, BoxGroup("Labels")]
    private TextMeshProUGUI coinsLabel;
    [SerializeField, SceneObjectsOnly, BoxGroup("Labels")]
    private TextMeshProUGUI healthLabel;
    [SerializeField, SceneObjectsOnly, BoxGroup("Labels")]
    private TextMeshProUGUI waveLabel;

    [SerializeField, BoxGroup("Panels")]
    private UIView gameOverPanel;
    [SerializeField, BoxGroup("Panels")]
    private UIView gameWonPanel;
    [SerializeField, BoxGroup("Panels")]
    private UIView gameHUD;
    [SerializeField, BoxGroup("Panels")]
    private UIView pausePanel;

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
