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

    public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
    public int MaxEnemies { get => maxEnemies;}
    public GameObject EnemyPrefab { get => enemyPrefab;}
}

public class EnemySpawner : MonoBehaviour
{
    [TitleGroup("References")]
    [SerializeField, Required, SceneObjectsOnly]
    private GameObject navMeshTarget;
    [TitleGroup("Waves")]
    [SerializeField]
    private Wave[] waves;
    [TitleGroup("Settings")]
    [SerializeField]
    private int timeBetweenWaves = 5;
    [SerializeField]
    private float randomSpawnMin = 0.1f;
    [SerializeField]
    private float randomSpawnMax = 3f;

    [FoldoutGroup("Debug Info")]
    [SerializeField,ReadOnly]
    private float lastSpawnTime;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    private int enemiesSpawned = 0;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    private bool isEnemyInLevel;
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    private bool isLastWave;

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

        StartCoroutine(Timer(randomSpawnMin, randomSpawnMax));
    }

    private IEnumerator Timer(float minInterval, float maxInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            currentWave = LevelManager.GetWave();
            isEnemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
            isMaxEnemiesReached = enemiesSpawned >= waves[currentWave].MaxEnemies;
            isLastWave = currentWave >= waves.Length -1;
            
            if (!isMaxEnemiesReached)
            {
                SpawnEnemy();
            }
            else if (isMaxEnemiesReached && !isEnemyInLevel && !isLastWave)
            {
                GoToNextWave();
            }
            else if (isLastWave && isMaxEnemiesReached && !isEnemyInLevel)
            {               
                GameStatusManager.SetStatus(GameStates.GameWon, this);
                yield break;
            }
        }

    }

    private void GoToNextWave()
    {
        LevelManager.SetWave(LevelManager.GetWave() + 1);
        LevelManager.SetCoins(Mathf.RoundToInt(LevelManager.GetCoins() * 1.1f));
        enemiesSpawned = 0;
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(waves[currentWave].EnemyPrefab, transform);
        Enemy neComp = newEnemy.GetComponent<Enemy>();
        neComp.NavMeshTarget = navMeshTarget;
        enemiesSpawned++;
    }
}
