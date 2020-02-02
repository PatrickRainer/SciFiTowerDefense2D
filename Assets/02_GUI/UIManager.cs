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
    [SerializeField]
    private GameObject pausePanel;

    private void Start()
    {
        gameHUD.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        // Event Listneners
        Message.AddListener<GameEventMessage>("GameOver", OnGameOver);
        Message.AddListener<GameEventMessage>("GameWon", OnGameWon);
        Message.AddListener<GameEventMessage>("PauseGame", OnGamePause);
        Message.AddListener<GameEventMessage>("UnPauseGame", OnUnPauseGame);
    }
       
    private void OnDisable()
    {
        Message.RemoveListener<GameEventMessage>("GameOver", OnGameOver);
        Message.RemoveListener<GameEventMessage>("GameWon", OnGameWon);
        Message.RemoveListener<GameEventMessage>("PauseGame", OnGamePause);
        Message.AddListener<GameEventMessage>("UnPauseGame", OnUnPauseGame);
    }

    private void OnGamePause(GameEventMessage obj)
    {
        ShowPausePanel();
    }

    private void OnUnPauseGame(GameEventMessage obj)
    {
        HidePausePanel();
    }

    private void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    private void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    private void OnGameWon(GameEventMessage obj)
    {
        gameWonPanel.SetActive(true);
    }

    private void OnGameOver(GameEventMessage message)
    {
        ShowGameOverPanel();      
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        HideGameHUD();
        GameEventMessage.SendEvent("PauseGame");
    }

    private void ShowGameWonPanel()
    {
        gameWonPanel.SetActive(true);
        HideGameHUD();
        GameEventMessage.SendEvent("PauseGame");
    }

    private void HideGameHUD()
    {
        gameHUD.SetActive(false);
    }
   
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
