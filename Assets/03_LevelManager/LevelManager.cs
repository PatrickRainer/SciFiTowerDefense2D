using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Doozy.Engine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int coinsPerDestroyedEnemy = 50;
    [SerializeField]
    private int initialCoins = 500;
    [SerializeField]
    private int initialPlayerHealth = 4;
    private static int coins = 0;
    private static int playerHealth = 0;
    private static int wave = 0;


    public static int GetCoins()
    {
        return coins;
    }

    public static void SetCoins(int value)
    {
        coins = value;
    }

    public static void IncreaseCoins(int value)
    {
        coins += value;
    }

    public static int GetWave()
    {
        return wave;
    }

    public static void SetWave(int value)
    {
        wave = value;
    }

    private void Start()
    {
        SetCoins(initialCoins);
        PlayerHealth = initialPlayerHealth;
    }

    private void OnEnable()
    {
        // Event Listneners
        Message.AddListener<GameEventMessage>("EnemyDestroyed", OnEnemyDestroyed);
    }

    private void OnDisable()
    {
        // Remove listneners
        Message.RemoveListener<GameEventMessage>("EnemyDestroyed", OnEnemyDestroyed);
    }

    public static int PlayerHealth
    {
        get => playerHealth;
        set
        {
            playerHealth = value;
            if (playerHealth <= 0)
            {
                GameStatusManager.Status = GameStates.GameLost;
            }
        }
    }

    private void OnEnemyDestroyed(GameEventMessage message)
    {
        SetCoins(GetCoins() + coinsPerDestroyedEnemy);
    }
}
