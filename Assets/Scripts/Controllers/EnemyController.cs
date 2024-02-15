using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
    private float MinSpawnInterval;

    [SerializeField]
    private float MaxSpawnInterval;

    private Vector3 InitialPos;
    private int SpawnedWalkerCount, MaxWalkerCount;

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
        StartSendingEnemies();
        //InvokeRepeating("SendEnemy", InitialWaitTime.Value, EnemyRepeatRate.Value);
    }

    void StartSendingEnemies() {
        Dictionary<EnemyType, int> req = EnemyWaveSettings.LevelRequirement[1];

        foreach (KeyValuePair<EnemyType, int> r in req)
        {
            switch (r.Key) {
                case EnemyType.WALKER:
                    SpawnedWalkerCount = 0;
                    MaxWalkerCount = r.Value;
                    SpawnWalkerWithDelay();
                    break;
                case EnemyType.FLOATING:
                    break;
                default:
                    break;
            }
        }
    }

    void SpawnWalkerWithDelay()
    {
        if (SpawnedWalkerCount >= MaxWalkerCount)
        {
            Debug.Log("Nothing more to spawn");
            return;
        }

        float spawnDelay = Random.Range(MinSpawnInterval, MaxSpawnInterval);

        Invoke("SpawnWalker", spawnDelay);
    }

    void SpawnWalker()
    {
        GameObject newEnemy = WalkerEnemyPool.ObjectPool.Get();
        newEnemy.transform.position = InitialPos;
        newEnemy.SetActive(true);

        SpawnedWalkerCount++;

        SpawnWalkerWithDelay();
    }
}
