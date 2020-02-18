using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Wave
{
    [SerializeField, AssetsOnly]
    private GameObject enemyPrefab;
    [SerializeField]
    float spawnInterval;
    [SerializeField]
    private int maxEnemies;

    public float SpawnInterval { get => spawnInterval;}
    public int MaxEnemies { get => maxEnemies;}
    public GameObject EnemyPrefab { get => enemyPrefab;}

}

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField, Required, SceneObjectsOnly]
    private GameObject navMeshTarget;
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private int timeBetweenWaves = 5;
    [SerializeField,ReadOnly]
    private float lastSpawnTime;
    [SerializeField, ReadOnly]
    private int enemiesSpawned = 0;

    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    private int currentWave;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    private int wavesLength;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    float timeInterval;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    float spawnInterval;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    bool isEnemySpawned;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    bool isTimeToSpawn;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    bool isMaxEnemiesReached;

    private void Start()
    {
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        wavesLength = waves.Length; // Only used for Debug reasons

        currentWave = LevelManager.GetWave();
        
        if (currentWave < waves.Length)
        {

            timeInterval = Time.time - lastSpawnTime;
            spawnInterval = waves[currentWave].SpawnInterval;

            isEnemySpawned = enemiesSpawned != 0 && timeInterval < timeBetweenWaves;
            isTimeToSpawn = timeInterval > spawnInterval;
            isMaxEnemiesReached = enemiesSpawned >= waves[currentWave].MaxEnemies;
            if ((!isEnemySpawned && isTimeToSpawn) && !isMaxEnemiesReached)
            {
                lastSpawnTime = Time.time;
                GameObject newEnemy = Instantiate(waves[currentWave].EnemyPrefab, transform);
                Enemy neComp = newEnemy.GetComponent<Enemy>();
                neComp.NavMeshTarget = navMeshTarget;
                enemiesSpawned++;
            }

            bool isEnemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
            if (isMaxEnemiesReached && !isEnemyInLevel)
            {
                LevelManager.SetWave(LevelManager.GetWave() + 1);
                LevelManager.SetCoins(Mathf.RoundToInt(LevelManager.GetCoins() * 1.1f));
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            GameStatusManager.SetStatus(GameStates.GameWon, this);
            this.gameObject.SetActive(false);
        }
    }

}
