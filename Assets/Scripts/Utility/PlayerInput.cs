using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private GameEvent PlayerTapInput;

    [SerializeField]
    private Vector3Variable CannonPos;

    [SerializeField]
    private Vector3Variable MouseTapPos;

    [SerializeField]
    private LineRenderer arcRenderer;

    [SerializeField]
    private Camera MainCamera;

    [SerializeField]
    public FloatVariable InitialCannonVelocity;

    [SerializeField]
    public FloatVariable CannonLaunchAngle;

    public float MaxLaunchAngle = 80;
    public float MinLaunchAngle = -45;

    public float timeInterval = 0.1f;
    public float gravity = 9.81f;

    public int arcResolution = 200;

    private void Start()
    {
        CannonLaunchAngle.Value = 45;
    }
    void Update()
    {
        GetPlayerInput();
    }

    /// <summary>
    /// Takes input from player
    /// updates the position of tap
    /// fires tap event
    /// </summary>
    void GetPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseTapPos.Value = Input.mousePosition;
            DrawArc();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseTapPos.Value = Input.mousePosition;
            PlayerTapInput.Raise();
            arcRenderer.positionCount = 0;
        }

        float xPos = Input.GetAxis("Mouse X");
        if (Input.GetMouseButton(0)) {
            //MouseTapPos.Value = Input.mousePosition;
            if (xPos > .2) {
                Debug.Log("Increase launch angle");
                arcRenderer.positionCount = 0;
                IncreaseLaunchAngle();
                DrawArc();
            }
            else if (xPos < -.2) {
                Debug.Log("Decrease launch angle");
                arcRenderer.positionCount = 0;
                DecreaseLaunchAngle();
                DrawArc();
            }
                
        }


    }
    void IncreaseLaunchAngle() {
        CannonLaunchAngle.Value = Mathf.Clamp(CannonLaunchAngle.Value + 5, MinLaunchAngle, MaxLaunchAngle);
    }
    void DecreaseLaunchAngle()
    {
        CannonLaunchAngle.Value = Mathf.Clamp(CannonLaunchAngle.Value - 5, MinLaunchAngle, MaxLaunchAngle);
    }

    void DrawArc()
    {
        arcRenderer.positionCount = 0;
        arcRenderer.SetPositions(new Vector3[0]);

        arcRenderer.positionCount = 100;
        Vector3[] points = new Vector3[100];

        float angle = CannonLaunchAngle.Value * Mathf.Deg2Rad;
        float v0x = InitialCannonVelocity.Value * Mathf.Cos(angle);
        float v0y = InitialCannonVelocity.Value * Mathf.Sin(angle);

        float time = 0f;
        for (int i = 0; i < 100; i++)
        {
            float x = CannonPos.Value.x + v0x * time;
            float y = CannonPos.Value.y + ((v0y * time) - (0.5f * gravity * time * time));
            points[i] = new Vector3(x, y, 0);

            time += timeInterval;
        }

        arcRenderer.SetPositions(points);
    }

}
