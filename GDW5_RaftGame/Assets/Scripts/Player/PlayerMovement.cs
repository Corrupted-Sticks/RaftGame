using SDS_Locations;
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

    [SerializeField] bool isController = false;

    InputDevice _device;

    public Rigidbody RB { get => _rb; }
    public Collider Collider { get => _collider; }
    [SerializeField] private Animator _animator;

    [SerializeField] Transform _cameraPivot;
    public Rigidbody hipBone;
    public Transform HipBone { get { return hipBone.transform; } }
    Camera _camera;

    Trigger_SailControl _sailControl;

    Vector3 rawMoveDir = Vector3.zero;
    Vector2 heading = new Vector2(0, 0);
    Vector2 camSensitivity = new Vector2(1f, 1f);

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

    public bool canMove = true;
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
    void Awake() {
        _Input = new PlayerInput();        // Create asset object.
        _PActions = _Input.Player;         // Extract action map object.
        _PActions.AddCallbacks(this);      // Register callback interface IPlayerActions.

        if (isController) {
            _device = Gamepad.current;
            _Input.devices = new InputDevice[] { Gamepad.current };
        } else {
            _device = Keyboard.current;
            _Input.devices = new InputDevice[] { Keyboard.current, Mouse.current };
        }

        _camera = Camera.main;
        _sailControl = FindFirstObjectByType<Trigger_SailControl>();

        ActiveRagdoll[] tests = FindObjectsOfType(typeof(ActiveRagdoll)) as ActiveRagdoll[];
        foreach (var t in tests) {
            print(t.gameObject.name);// do something 
}
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!canMove || ctx.control.device != _device) return;
        Vector2 raw = ctx.ReadValue<Vector2>();
        //moveDir = new Vector3(raw.x, 0.0f, raw.y);
        //moveDir = Quaternion.Euler(_camera.transform.rotation.x, 0, 0) * _camera.transform.forward.normalized * raw.y + Quaternion.Euler(0, _camera.transform.rotation.y, 0) * _camera.transform.right.normalized * raw.x;
        rawMoveDir = new Vector3(raw.x, 0f, raw.y);
        
    }
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.started || ctx.control.device != _device) return;
        if (_sailControl.playersInside > 0)
        {
            if (_sailControl.isPlayerControlling)
            {
                _sailControl.ExitControl(this);

            }
            else
            {
                _sailControl.TakeControl(this);


            }
        }
    }
    public void OnRightArm(InputAction.CallbackContext ctx) { }
    public void OnLeftArm(InputAction.CallbackContext ctx) { }
    public void OnLook(InputAction.CallbackContext ctx)
    {
        if (ShopManager.instance.isShown) return;
        heading.x += ctx.ReadValue<Vector2>().x * Time.deltaTime * 6f * camSensitivity.x;
        heading.y += ctx.ReadValue<Vector2>().y * Time.deltaTime * 10f * camSensitivity.y;
        heading.y = Mathf.Clamp(heading.y, -30, 30);
        _cameraPivot.rotation = Quaternion.Euler(-heading.y, heading.x, 0);
    }

    public void OnMenu(InputAction.CallbackContext ctx)
    {
        PauseManager.instance.ToggleUI();

        GameManager.instance.CallPause();
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
        //_animator = GetComponent<Animator>();
        _maxSpeedSquared = _MaxSpeed * _maxSpeed;
    }


    public void OnReset(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;
        hipBone.transform.position = BoatController.instance.RespawnPosition.position;
    }

    private void OnTriggerEnter(Collider other) {
            
    }
    private void Update() {


        if (rawMoveDir == Vector3.zero) {
            _animator.SetFloat("Speed", 0);
        } else {
            _animator.SetFloat("Speed", 0.5f);

            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.01f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.2f);
            //transform.rotation = Quaternion.Euler(0,_camera.transform.rotation.eulerAngles.y,0);

            //transform.Translate(moveDir * _rb.linearVelocity.magnitude * Time.deltaTime, Space.World);

            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 5f);
            //transform.Translate(Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0) * moveDir * 3f * Time.deltaTime, Space.World);
            Quaternion yawRotation = Quaternion.Euler(
                0f,
                _camera.transform.eulerAngles.y,
                0f
            );

            moveDir = yawRotation * rawMoveDir;

            HipBone.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0),
                Time.deltaTime * 5f
            );
            var speed = _acceleration * Time.deltaTime * moveDir;
            transform.Translate(speed, Space.World);


        }
    }
}
