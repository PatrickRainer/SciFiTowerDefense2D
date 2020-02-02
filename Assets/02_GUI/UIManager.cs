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

    private void Start()
    {
        gameHUD.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);        
    }

    private void OnEnable()
    {
        // Event Listneners
        Message.AddListener<GameEventMessage>("GameOver", OnGameOver);
        Message.AddListener<GameEventMessage>("GameWon", OnGameWon);
    }

    private void OnGameWon(GameEventMessage obj)
    {
        gameWonPanel.SetActive(true);
        GameManager.PauseGame();
    }

    private void OnDisable()
    {
        Message.RemoveListener<GameEventMessage>("GameOver", OnGameOver);
        Message.RemoveListener<GameEventMessage>("GameWon", OnGameWon);
    }

    private void OnGameOver(GameEventMessage message)
    {
        ShowGameOverPanel();      
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        HideGameHUD();
        GameManager.PauseGame();
    }

    private void ShowGameWonPanel()
    {
        gameWonPanel.SetActive(true);
        HideGameHUD();
        GameManager.PauseGame();
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
