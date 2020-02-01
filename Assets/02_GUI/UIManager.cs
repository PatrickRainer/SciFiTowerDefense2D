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

    private void OnEnable()
    {
        // Event Listneners
        Message.AddListener<GameEventMessage>("GameOver", ShowGameOverPanel);
        Message.AddListener<GameEventMessage>("GameWon", OnGameWon);
    }

    private void OnGameWon(GameEventMessage obj)
    {
        gameWonPanel.SetActive(true);
        GameManager.PauseGame();
    }

    private void OnDisable()
    {
        Message.RemoveListener<GameEventMessage>("GameOver", ShowGameOverPanel);
        Message.RemoveListener<GameEventMessage>("GameWon", OnGameWon);
    }

    private void ShowGameOverPanel(GameEventMessage message)
    {
        gameOverPanel.SetActive(true);
        GameManager.PauseGame();
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
