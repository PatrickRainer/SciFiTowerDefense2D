using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Wave
{
    [SerializeField, AssetsOnly]
    public GameObject[] enemyPrefabs;
    [SerializeField]
    public float randomSpawnMin = 0.1f;
    [SerializeField]
    public float randomSpawnMax = 3f;

    [SerializeField]
    public int maxEnemies;

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
    [FoldoutGroup("Debug Info")]
    [SerializeField, ReadOnly]
    bool isTowerInLevel;

    private void Start()
    {
        lastSpawnTime = Time.time;

        StartCoroutine(Timer(waves[currentWave].randomSpawnMin, waves[currentWave].randomSpawnMax));
    }

    private IEnumerator Timer(float minInterval, float maxInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            currentWave = LevelManager.GetWave();
            isEnemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
            isTowerInLevel = GameObject.FindGameObjectsWithTag("Tower").Length > 0;
            isMaxEnemiesReached = enemiesSpawned >= waves[currentWave].maxEnemies;
            isLastWave = currentWave >= waves.Length -1;
            
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
        GameObject randomPrefab = waves[currentWave].enemyPrefabs[Random.Range(0, waves[currentWave].enemyPrefabs.Length-1)];
        GameObject newEnemy = Instantiate(randomPrefab, transform);
        Enemy neComp = newEnemy.GetComponent<Enemy>();
        neComp.NavMeshTarget = navMeshTarget;
        enemiesSpawned++;
    }
}
