using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCMovement : MonoBehaviour
{
    Rigidbody rb;
    public Rigidbody RB { get => rb; }

    [SerializeField] public Transform boatTransform;
    [SerializeField] public float acceleration = 0;
    [SerializeField] public float maxSpeed = 10;

    int commandInt;

    public bool isOnBoat { get; private set; }
    bool inputMoving = false;
    bool canInput = true;

    public Vector3 moveDir;
    public Vector3 playerVelocity;
    public Vector3 finalMoveDir;
    private Animator animator;
    public Collider Collider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Collider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        inputMoving = false;

        if (canInput)
        {
            if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[0])) // Move Forwards
            {
                commandInt = 0;
                CallExecute();
                inputMoving = true;
            }
            if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[1])) // Move Backwards
            {
                commandInt = 1;
                CallExecute();
                inputMoving = true;
            }
            if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[2])) // Move Left
            {
                commandInt = 2;
                CallExecute();
                inputMoving = true;
            }
            if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[3])) // Move Right
            {
                commandInt = 3;
                CallExecute();
                inputMoving = true;
            }
            if (Input.GetKeyDown(PlayerCommands.PCInstance.keyBinds[4])) // Interact
            {
                commandInt = 4;
                CallExecute();
                inputMoving = true;
            }
        }
    }

    private void Update() // Right now this is purely to go back to main menu. Will be changed later
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoader.Instance.LoadScene("MainMenu");
        }

        if (moveDir != Vector3.zero && inputMoving)
        {
            animator.SetFloat("Speed", 0.5f);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir.normalized), 0.15f);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void CallExecute()
    {
        PlayerCommands.PCInstance.keyCommands[commandInt].Execute(this);
    }

    public Rigidbody GetRB()
    {
        return rb;
    }

    public void ToggleInput(bool toggle)
    {
        canInput = toggle;
    }
}
