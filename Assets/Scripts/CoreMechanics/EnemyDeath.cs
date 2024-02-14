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
    private GameEventWithStr EnemyDeathEvent;

    [SerializeField]
    private IntVariable Score;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground")
        {
            Score.Value++;
            EnemyKilledEvent.Raise();
            EnemyDeathEvent.InvokeEvent(Type.ToString());
            EnemyPool.ObjectPool.Release(gameObject);
        }
    }
}
