using Sirenix.OdinInspector;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, PlayerInput.IPlayerActions
{
    PlayerInput _Input;                  // Source code representation of asset.
    PlayerInput.PlayerActions _PActions;     // Source code representation of action map.

    Rigidbody _rb;
    Collider _collider;
    public Rigidbody RB { get => _rb; }
    public Collider Collider { get => _collider; }

    [FoldoutGroup("Ground Movement")][SerializeField] float _acceleration = 0;
    [FoldoutGroup("Ground Movement")][SerializeField] float _maxSpeed = 10;

    float _MaxSpeed
    {
        get => _maxSpeed;
        set
        {
            _maxSpeed = value;
            _maxSpeedSquared = value * value;
        }
    }
    float _maxSpeedSquared;

    public bool isOnBoat
    {
        get => _isOnBoat;
        set { }
    }
    [FoldoutGroup("Ground Movement")][SerializeField] bool _isOnBoat;
    /// <summary>
    /// used to modify move direction to accurately move the player with respect to the boat's rotation from buoyancy
    /// </summary>
    [FoldoutGroup("Ground Movement")][SerializeField] Transform boatTransform;

    [FoldoutGroup("Debug Info")][SerializeField][ReadOnly] Vector3 moveDir;
#if UNITY_EDITOR     // DO NOT REFERENCE THESE, they aresimply for debugging, and will not exist in actual builds.

    [FoldoutGroup("Debug Info")][SerializeField][ReadOnly] Vector3 _playerVelocity;
#endif

    void OnEnable() => _Input.Enable();
    void OnDisable() => _Input.Disable();
    void OnDestroy() => _Input.Disable();
    void Awake()
    {
        _Input = new PlayerInput();        // Create asset object.
        _PActions = _Input.Player;         // Extract action map object.
        _PActions.AddCallbacks(this);      // Register callback interface IPlayerActions.
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 raw = ctx.ReadValue<Vector2>();
        moveDir = new Vector3(raw.x, 0.0f, raw.y);
    }
    public void OnInteract(InputAction.CallbackContext ctx)
    {

    }


    public void ToggleInput(bool value)
    {
        if (value) _Input.Enable();
        else _Input.Disable();
    }





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _maxSpeedSquared = _MaxSpeed * _maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        _playerVelocity = _rb.linearVelocity;
#endif

        if (moveDir.sqrMagnitude < 0.01f) return;

        // transform the world direction to be relative to the "boats" rotation. makes the player movement more accurately track the motion of the boat as it rocks.
        Vector3 finalMoveDir = _isOnBoat ? boatTransform.TransformDirection(moveDir.normalized) : moveDir;


        _rb.AddForce(finalMoveDir * _acceleration, ForceMode.Acceleration);

        _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, _MaxSpeed);




    }


    private void Update()
    {
        //DEBUG : MAKE USE INPUT SYSTEM/REMOVE LATER
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = BoatController.instance.RespawnPosition.position;
        }
    }
}
