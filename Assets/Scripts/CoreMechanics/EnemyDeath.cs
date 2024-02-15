using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    private EnemyType Type;

    [Tooltip("Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable EnemyPool;

    [SerializeField]
    private GameEvent EnemyKilledEvent;

    [SerializeField]
    private GameEvent CastleBreachedEvent;

    [SerializeField]
    private GameEventWithStr EnemyDeathEvent;

    [SerializeField]
    private IntVariable TotalKilledEnemy;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground")
        {
            TotalKilledEnemy.Value++;
            if (other.gameObject.tag == "Castle")
            {
                CastleBreachedEvent.Raise();
            }
            else {
                EnemyKilledEvent.Raise();
                EnemyDeathEvent.InvokeEvent(Type.ToString());
            }
            EnemyPool.ObjectPool.Release(gameObject);
        }
    }
}
