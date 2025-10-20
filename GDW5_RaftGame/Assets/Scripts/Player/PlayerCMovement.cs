using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float acceleration = 0;
    [SerializeField] float maxSpeed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(PlayerCommands.PCInstance.keyBinds[0])) // Move Forwards
        {
            PlayerCommands.PCInstance.keyCommands[0].Execute();
        }
        if (Input.GetKeyDown(PlayerCommands.PCInstance.keyBinds[1])) // Move Backwards
        {
            PlayerCommands.PCInstance.keyCommands[1].Execute();
        }
        if (Input.GetKeyDown(PlayerCommands.PCInstance.keyBinds[2])) // Move Left
        {
            PlayerCommands.PCInstance.keyCommands[2].Execute();
        }
        if (Input.GetKeyDown(PlayerCommands.PCInstance.keyBinds[3])) // Move Right
        {
            PlayerCommands.PCInstance.keyCommands[3].Execute();
        }
        if (Input.GetKeyDown(PlayerCommands.PCInstance.keyBinds[4])) // Interact
        {
            PlayerCommands.PCInstance.keyCommands[4].Execute();
        }
    }
}
