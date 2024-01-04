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

    private Rigidbody2D CannonRigidBody;

    private void Start()
    {
        CannonRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        IsFired = false;
    }

    public void Shoot() {

        // Rotate the cannon to face the target
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(MouseTapPos.Value);

        Vector3 cannonPosition = transform.position;
        Vector3 launchDirection = (targetPosition - cannonPosition).normalized;

        // Calculate the horizontal distance to the target
        //float horizontalDistance = Vector3.Distance(targetPosition, cannonPosition);

        // Calculate the initial velocity required to reach the target
        float gravity = 9.81f;
        //InitialVelocity.Value = Mathf.Sqrt(horizontalDistance * gravity / Mathf.Sin(2 * LaunchAngle.Value * Mathf.Deg2Rad));

        // Calculate the launch velocity components
        float horizontalVelocity = InitialVelocity.Value * Mathf.Cos(LaunchAngle.Value * Mathf.Deg2Rad);
        float verticalVelocity = InitialVelocity.Value * Mathf.Sin(LaunchAngle.Value * Mathf.Deg2Rad);

        // Apply the launch velocity to the cannon's Rigidbody
        CannonRigidBody.constraints = RigidbodyConstraints2D.None;
        CannonRigidBody.velocity = new Vector3(horizontalVelocity * launchDirection.x, verticalVelocity, 0);
    }

    public void UpdateCannonStatus() {
        IsFired = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collided " + other.gameObject.name);
        if (IsFired)
        {
            Debug.Log("Sending back to pool");
            IsFired = false;
            CannonPool.ObjectPool.Release(gameObject);
            CannonFiredEvent.Raise();
        }

    }

}
