using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private FloatVariable Speed;

    [Tooltip("Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable EnemyPool;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(-Speed.Value, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Ground")
            EnemyPool.ObjectPool.Release(gameObject);
    }
}
