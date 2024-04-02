using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private FloatVariable Speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(-Speed.Value, 0, 0);
    }

    public void SetSpeed(FloatVariable speed) {
        Speed = speed;
    }
}
