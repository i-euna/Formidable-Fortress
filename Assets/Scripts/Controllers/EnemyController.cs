using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Walker Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable WalkerEnemyPool;

    [Tooltip("Original Enemy")]
    [SerializeField]
    private GameObject WalkerEnemyPrefab;
    //List of enemies
    private Stack FiredEnemies;

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
    private int SpawnedWalkerCount, WalkerCount, HighSpeedWalkerCount;

    private void Start()
    {
        InitialPos = transform.position;

        //Initialize the pool
        WalkerEnemyPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(WalkerEnemyPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );
        InitialPos = WalkerEnemyPrefab.transform.position;

        TotalKilledEnemy.Value = 0;
        TotalSpawnedEnemy.Value = 0;

        StartSendingEnemies();
        //InvokeRepeating("SendEnemy", InitialWaitTime.Value, EnemyRepeatRate.Value);
    }

    void StartSendingEnemies() {
        Dictionary<EnemyType, int> req = EnemyWaveSettings.LevelRequirement[LevelManager.CurrentLevel];
        TotalSpawnedEnemy.Value = 0;

        //calculating total enemies to be spawned
        foreach (KeyValuePair<EnemyType, int> r in req)
        {
            switch (r.Key) {
                case EnemyType.WALKER:
                    SpawnedWalkerCount = 0;
                    WalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += WalkerCount;
                    //SpawnWalkerWithDelay();
                    break;
                case EnemyType.HIGH_SPEED_WALKER:
                    HighSpeedWalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += HighSpeedWalkerCount;
                    break;
                default:
                    break;
            }
        }

        int segmentLength = 3;
        int totalTimeNeeded = TotalSpawnedEnemy.Value * segmentLength;

        List<float> spawnIntervals = new List<float>();
        List<EnemyType> enemyTypes = new List<EnemyType>();
        spawnIntervals.Add(0);
        //calculate spawn points in time
        for (int i = 1; i < TotalSpawnedEnemy.Value; i++) {
            float spawnDelay = UnityEngine.Random.Range(MinSpawnInterval, MaxSpawnInterval);
            spawnIntervals.Add(spawnDelay);
        }

        Array enemyTypeValues = Enum.GetValues(typeof(EnemyType));
        enemyTypes.Add((EnemyType)enemyTypeValues.GetValue(0));

        for (int i = 1; i < TotalSpawnedEnemy.Value; i++)
        {

        }
}

    //void SpawnWalkerWithDelay()
    //{
    //    if (SpawnedWalkerCount >= WalkerCount)
    //    {
    //        Debug.Log("Nothing more to spawn");
    //        return;
    //    }

    //    float spawnDelay = Random.Range(MinSpawnInterval, MaxSpawnInterval);

    //    Invoke("SpawnWalker", spawnDelay);
    //}

    //void SpawnWalker()
    //{
    //    GameObject newEnemy = WalkerEnemyPool.ObjectPool.Get();
    //    newEnemy.transform.position = InitialPos;
    //    newEnemy.SetActive(true);

    //    SpawnedWalkerCount++;

    //    SpawnWalkerWithDelay();
    //}

    //void UpdateTotalSpawnedCount() {
    //    TotalSpawnedEnemy.Value = WalkerCount;
    //}
}
