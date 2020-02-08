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
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject gameWonPanel;
    [SerializeField]
    private GameObject gameHUD;
    [SerializeField, Required]
    private UIView pausePanel;

    private void Start()
    {
        //gameHUD.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        pausePanel.Hide();
    }

    private void Update()
    {
        if (GameStatusManager.Status == GameStates.GameLost)
        {
            gameOverPanel.SetActive(true);
            gameHUD.SetActive(false);
        }

        if (GameStatusManager.Status == GameStates.GameWon)
        {
            gameWonPanel.SetActive(true);
            gameHUD.SetActive(false);
        }

        if (GameStatusManager.Status == GameStates.GamePaused)
        {
            pausePanel.Show();
            gameHUD.SetActive(false);
        }

        if (GameStatusManager.Status == GameStates.GameRuns)
        {
            gameHUD.SetActive(true);
            gameOverPanel.SetActive(false);
            gameWonPanel.SetActive(false);
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
