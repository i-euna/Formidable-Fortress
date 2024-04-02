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
    private IntVariable LevelTotalKilled, LevelTotalDestroyed;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground" &&
            other.gameObject.tag != "Enemy")
        {
            LevelTotalDestroyed.Value++;
            if (other.gameObject.tag == "Castle")
            {
                CastleBreachedEvent.Raise();
            }
            else {
                LevelTotalKilled.Value++;
                EnemyKilledEvent.Raise();
                EnemyDeathEvent.InvokeEvent(Type.ToString());
            }
            EnemyPool.ObjectPool.Release(gameObject);
        }
    }

    public void SetType(EnemyType enemyType) {
        Type = enemyType;
    }
}
