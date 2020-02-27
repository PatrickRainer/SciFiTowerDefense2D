using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class enemySpawnData
{
    public GameObject enemyPrefab;
    public int chanceToSpawn = 34;
}


[System.Serializable]
public class Wave
{
    [TitleGroup("Wave Settings")]
    [SerializeField, MinMaxSlider(0.1f, 5f)]
    public Vector2 spawnInterval;
    [SerializeField]
    public int maxEnemies;

    [SerializeField, AssetsOnly]
    public enemySpawnData[] enemyObjects;
}

public class EnemySpawner : MonoBehaviour
{
    [TitleGroup("References & Settings")]
    [SerializeField, Required, SceneObjectsOnly]
    private GameObject navMeshTarget;
    [SerializeField]
    private int timeBetweenWaves = 5;

    [TitleGroup("Waves")]
    [SerializeField]
    private Wave[] waves;

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
    private int currentWaveIdx;
    private Wave currentWave { get => waves[currentWaveIdx]; }
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
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    bool isTowerInLevel;

    private void Start()
    {
        lastSpawnTime = Time.time;

        StartCoroutine(Timer(currentWave.spawnInterval.x, currentWave.spawnInterval.y));
    }

    private IEnumerator Timer(float minInterval, float maxInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            currentWaveIdx = LevelManager.GetWave();
            isEnemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
            isTowerInLevel = GameObject.FindGameObjectsWithTag("Tower").Length > 0;
            isMaxEnemiesReached = enemiesSpawned >= waves[currentWaveIdx].maxEnemies;
            isLastWave = currentWaveIdx >= waves.Length -1;
            
            if (!isMaxEnemiesReached && isTowerInLevel)
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
        List<int> weightsListed = new List<int>(); 
        foreach (enemySpawnData item in currentWave.enemyObjects)
        {
            weightsListed.Add(item.chanceToSpawn);
        }

        int spawnIdx = GetRandomWeightedIndex(weightsListed);

        GameObject newEnemy = Instantiate(currentWave.enemyObjects[spawnIdx].enemyPrefab, transform);
        Enemy neComp = newEnemy.GetComponent<Enemy>();
        neComp.NavMeshTarget = navMeshTarget;
        enemiesSpawned++;
    }

    // Gets a Index out of a list by weights so chance to spawn
    private int GetRandomWeightedIndex(List<int> weights)
    {
        if (weights == null || weights.Count == 0) return -1;

        int total = 0;
        int i;
        for (i = 0; i < weights.Count; i++)
        {
            total += weights[i];
        }

        float r = Random.value;
        float s = 0f;

        for (i = 0; i < weights.Count; i++)
        {
            if (weights[i] <= 0f) continue;

            s += (float)weights[i] / total;
            if (s >= r) return i;
        }

        return -1;
    }
}
