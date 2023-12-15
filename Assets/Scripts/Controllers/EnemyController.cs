using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable EnemyPool;

    [Tooltip("Original Enemy")]
    [SerializeField]
    private GameObject EnemyPrefab;
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

    private Vector3 InitialPos;

    private void Start()
    {
        InitialPos = transform.position;

        //Initialize the pool
        EnemyPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(EnemyPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );
        InitialPos = EnemyPrefab.transform.position;

        InvokeRepeating("SendEnemy", InitialWaitTime.Value, EnemyRepeatRate.Value);
    }

    private void SendEnemy() {
        GameObject newEnemy = EnemyPool.ObjectPool.Get();

        newEnemy.transform.position = InitialPos;

        newEnemy.SetActive(true);
    }
}
