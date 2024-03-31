using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Walker Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable WalkerPool;

    [Tooltip("Medium Speed Walker Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable MediumSpeedWalkerPool;

    [Tooltip("High Speed Walker Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable HighSpeedWalkerPool;

    [Tooltip("Walker")]
    [SerializeField]
    private GameObject WalkerEnemyPrefab;

    [Tooltip("Medium speed walker")]
    [SerializeField]
    private GameObject MediumSpeedWalkerPrefab;

    [Tooltip("High speed walker")]
    [SerializeField]
    private GameObject HighSpeedWalkerPrefab;

    [Tooltip("Max no of enemy in the pool")]
    [SerializeField]
    private IntVariable MaxNoOfEnemies;

    [Tooltip("Initial Wait Time for Enemy Invoker")]
    [SerializeField]
    private FloatVariable InitialWaitTime;

    [Tooltip("Repeat Rate for Enemy Invoker")]
    [SerializeField]
    private FloatVariable EnemyRepeatRate;

    [SerializeField]
    private IntVariable TotalSpawnedEnemy, TotalKilledEnemy;

    [SerializeField]
    private float MinSpawnInterval;

    [SerializeField]
    private float MaxSpawnInterval;

    private Vector3 InitialPos;
    private int SpawnedWalkerCount, SlowSpeedWalkerCount, MediumSpeedWalkerCount, HighSpeedWalkerCount;
    List<float> spawnIntervals;
    List<EnemyType> enemyTypes;
    private void Start()
    {
        InitializeEnemyPools();
        InitialPos = WalkerEnemyPrefab.transform.position;

        TotalKilledEnemy.Value = 0;
        TotalSpawnedEnemy.Value = 0;

        PrepareEnemySequence();
        SpawnedWalkerCount = 0;
        SpawnWithDelay();
    }

    void InitializeEnemyPools() {
        //Initialize the pool
        WalkerPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(WalkerEnemyPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );

        //Debug.Log("MediumSpeedWalkerPool " + MediumSpeedWalkerPool.ObjectPool);
        MediumSpeedWalkerPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(MediumSpeedWalkerPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );

        HighSpeedWalkerPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(HighSpeedWalkerPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );
    }

    void PrepareEnemySequence() {
        Dictionary<EnemyType, int> req = EnemyWaveSettings.LevelRequirement[LevelManager.GetCurrentLevel()];
        TotalSpawnedEnemy.Value = 0;

        spawnIntervals = new List<float>();
        enemyTypes = new List<EnemyType>();
        //calculating total enemies to be spawned
        foreach (KeyValuePair<EnemyType, int> r in req)
        {
            switch (r.Key) {
                case EnemyType.SLOW_SPEED_WALKER:
                    SpawnedWalkerCount = 0;
                    SlowSpeedWalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += SlowSpeedWalkerCount;
                    ListManipulator.AddMultipleTimes(
                            enemyTypes, 
                            EnemyType.SLOW_SPEED_WALKER, 
                            r.Value
                        );
                    break;
                case EnemyType.MEDIUM_SPEED_WALKER:
                    MediumSpeedWalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += MediumSpeedWalkerCount;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.MEDIUM_SPEED_WALKER,
                            r.Value
                        );
                    break;
                case EnemyType.HIGH_SPEED_WALKER:
                    HighSpeedWalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += HighSpeedWalkerCount;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes, 
                            EnemyType.HIGH_SPEED_WALKER, 
                            r.Value
                        );
                    break;
                default:
                    break;
            }
        }
        enemyTypes = ListManipulator.ShuffleList(enemyTypes);

        //prepare spawn intervals
        spawnIntervals.Add(0);
        for (int i = 1; i < TotalSpawnedEnemy.Value; i++) {
            float spawnDelay = UnityEngine.Random.Range(MinSpawnInterval, MaxSpawnInterval);
            spawnIntervals.Add(spawnDelay);
        }

        Debug.Log("total spawned enemy " + TotalSpawnedEnemy.Value);
}

    void SpawnWithDelay()
    {
        if (SpawnedWalkerCount >= TotalSpawnedEnemy.Value)
        {
            Debug.Log("Nothing more to spawn");
            return;
        }

        float spawnDelay = spawnIntervals[SpawnedWalkerCount];
        Debug.Log(" spawnDelay " + spawnDelay);
        Invoke("SpawnEnemy", spawnDelay);
    }

    void SpawnEnemy()
    {
        EnemyType enemyType = enemyTypes[SpawnedWalkerCount];

        GameObject newEnemy;
        switch (enemyType) {
            case EnemyType.SLOW_SPEED_WALKER:
                newEnemy = WalkerPool.ObjectPool.Get();
                break;
            case EnemyType.MEDIUM_SPEED_WALKER:
                newEnemy = MediumSpeedWalkerPool.ObjectPool.Get();
                break;
            case EnemyType.HIGH_SPEED_WALKER:
                newEnemy = HighSpeedWalkerPool.ObjectPool.Get();
                break;
            default:
                newEnemy = WalkerPool.ObjectPool.Get();
                break;
        }
        
        newEnemy.transform.position = InitialPos;
        newEnemy.SetActive(true);

        SpawnedWalkerCount++;

        SpawnWithDelay();
    }

}
