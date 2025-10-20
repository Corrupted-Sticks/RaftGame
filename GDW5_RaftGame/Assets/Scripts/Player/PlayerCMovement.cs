using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] public Transform boatTransform;
    [SerializeField] public float acceleration = 0;
    [SerializeField] public float maxSpeed = 10;

    int commandInt;

    public bool isOnBoat { get; private set; }

    public Vector3 moveDir;
    public Vector3 playerVelocity;
    public Vector3 finalMoveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[0])) // Move Forwards
        {
            commandInt = 0;
            CallExecute();
        }
        if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[1])) // Move Backwards
        {
            commandInt = 1;
            CallExecute();
        }
        if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[2])) // Move Left
        {
            commandInt = 2;
            CallExecute();
        }
        if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[3])) // Move Right
        {
            commandInt = 3;
            CallExecute();
        }
        if (Input.GetKey(PlayerCommands.PCInstance.keyBinds[4])) // Interact
        {
            commandInt = 4;
            CallExecute();
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
}
