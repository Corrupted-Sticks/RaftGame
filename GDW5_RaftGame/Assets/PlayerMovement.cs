using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, PlayerInput.IPlayerActions
{
    PlayerInput _Input;                  // Source code representation of asset.
    PlayerInput.PlayerActions _PActions;     // Source code representation of action map.

    Rigidbody _rb;


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






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _maxSpeedSquared = _MaxSpeed * _maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        _playerVelocity = _rb.linearVelocity;
#endif

        if (moveDir.sqrMagnitude < 0.01f) return;

        _rb.AddForce(moveDir * _acceleration, ForceMode.Acceleration);

        _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, _MaxSpeed);




    }
}
