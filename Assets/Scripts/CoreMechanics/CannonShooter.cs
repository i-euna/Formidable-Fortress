using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    [Tooltip("Cannon pool")]
    [SerializeField]
    private ObjectPoolVariable CannonPool;

    [Tooltip("Speed of bullet")]
    [SerializeField]
    private FloatVariable CannonSpeed;

    [Tooltip("Last tap position of tap")]
    [SerializeField]
    private Vector3Variable MouseTapPos;

    [SerializeField]
    private GameEvent CannonFiredEvent;

    private bool IsFired;

    [Tooltip("Default launch angle")]
    [SerializeField]
    private FloatVariable LaunchAngle;
    [Tooltip("Default initial velocity")]
    [SerializeField]
    private FloatVariable InitialVelocity;

    private Rigidbody2D Rb;

    private Vector3 TargetPosition;
    private float ElapsedTime = 0f;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        IsFired = false;
    }

    public void Shoot() {

        Vector3 TargetPosition = Camera.main.ScreenToWorldPoint(MouseTapPos.Value);
       
        TargetPosition.z = transform.position.z;
        Rb.constraints = RigidbodyConstraints2D.None;
        IsFired = true;
        //Vector3 cannonPosition = transform.position;
        //Vector3 shootDirection = (targetPosition - cannonPosition).normalized;
    }

    private void Update()
    {
        if(IsFired)
            Move();
    }
    void Move()
    {
        Vector3 TargetPosition = Camera.main.ScreenToWorldPoint(MouseTapPos.Value);
        TargetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref velocity, 0.5f);
    }

    public void UpdateCannonStatus() {
        IsFired = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsFired)
        {
            IsFired = false;
            CannonPool.ObjectPool.Release(gameObject);
            CannonFiredEvent.Raise();
        }

    }

}
