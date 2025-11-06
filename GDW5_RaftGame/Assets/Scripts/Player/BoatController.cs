using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour, PlayerInput.IPlayerActions
{

    public static BoatController instance;
    PlayerInput _Input;                  // Source code representation of asset.
    PlayerInput.PlayerActions _PActions;     // Source code representation of action map.


    Rigidbody _rb;

    Vector3 moveDir = Vector3.zero;
    public float acceleration;
    const float minWindMultiplier = 0.75f;
    public float maxSpeed;
    public float rotationSpeed;
    public float boatRotationSpeed;

    public bool isDockDown = false;
    /// <summary>
    /// the actual sail that controlls the direction of the boat.
    /// </summary>
    public Transform SteeringSail;
    public Transform RespawnPosition;
    public GameObject dock;


#if UNITY_EDITOR
    [ShowInInspector] float currentSpeed;

#endif

    void OnDisable() => _Input.Disable();
    void OnDestroy() => _Input.Disable();
    void Awake()
    {
        _Input = new PlayerInput();        // Create asset object.
        _PActions = _Input.Player;         // Extract action map object.
        _PActions.AddCallbacks(this);      // Register callback interface IPlayerActions.

        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    public void ToggleInput(bool value)
    {
        if (value) _Input.Enable();
        else _Input.Disable();

    }


    void FixedUpdate()
    {
        // rotation (left/right movement)
        if (Mathf.Abs(moveDir.x) > 0.01f)
            SteeringSail.transform.Rotate(Vector3.up, moveDir.x*rotationSpeed);


        // movement  (forward/backwards)
        Vector3 sailForward = SteeringSail.forward;
        // project to xz plane to ignore sails tilt "up/down". without this any deviation from perfectly upright will cause the boat to over rotate and sink.
        var trueForwards = Vector3.ProjectOnPlane(sailForward, Vector3.up).normalized;

        Vector3 windDir = WindManager.instance.WindDirection;
        // at minimum, move half speed in opposite direction to the wind. closer to wind direction you point the sail, the faster it goes.
        float windFactor = minWindMultiplier + Mathf.Max(.35f, Vector3.Dot(trueForwards, windDir));

        Vector3 forwardForce = trueForwards * moveDir.z * acceleration * windFactor;
        _rb.AddForce(forwardForce, ForceMode.Force);
        _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, maxSpeed);

     
        if (_rb.linearVelocity.sqrMagnitude > 2) 
            RotateBoatToFaceSailDirection();


#if UNITY_EDITOR
        currentSpeed = _rb.linearVelocity.magnitude;
#endif




    }

    void RotateBoatToFaceSailDirection()
    {
        // Get the sail's local y rotation relative to the boat
        float sailYRotation = SteeringSail.eulerAngles.y;
        float boatLocalY = transform.eulerAngles.y;

        // Use DeltaAngle to get signed shortest difference (-180 to 180)
        float angleDiff = Mathf.DeltaAngle(boatLocalY, sailYRotation);

        // If the sail is rotated too far left/right, rotate the boat to compensate
        if (Mathf.Abs(angleDiff) > 2.5f) // 5 degree threshold
        {
            // Rotation direction is just sign of angleDiff now
            float speedFactor = Mathf.Max(0.75f, 1.15f*(_rb.linearVelocity.magnitude/maxSpeed)); // increase rotation speed based on current velocity
            float maxStep = boatRotationSpeed * Time.fixedDeltaTime;
            float rotationAmount = Mathf.Clamp(angleDiff, -maxStep, maxStep);

            // Rotate the boat and counter-rotate the sail to keep it "stationary" in world space
            transform.Rotate(0, rotationAmount, 0);
            SteeringSail.Rotate(0, -rotationAmount, 0);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 raw = ctx.ReadValue<Vector2>();
        moveDir = new Vector3(raw.x, 0.0f, raw.y);
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {

    }

    public void ToggleDock() => dock.SetActive(isDockDown = !isDockDown);
    public void ToggleDock(bool value)
    {
        dock.SetActive(value);
        isDockDown = value;
    }
    void OnDrawGizmosSelected()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();

        // Get the world position of the center of mass
        Vector3 comWorld = _rb.worldCenterOfMass;

        // Draw a small yellow sphere at the center of mass
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(comWorld, 0.1f);

        // Optional: draw a line from transform to COM
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, comWorld);
    }

}
