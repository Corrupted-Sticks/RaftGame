using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCMovement : MonoBehaviour
{
    private PCommand moveUp, moveDown, moveLeft, moveRight, interact;

    Dictionary<KeyCode, PCommand> defaultBind = new();
    Dictionary<KeyCode, PCommand> newBind = new();

    KeyCode key;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveUp = new MoveFCommand();
        moveDown = new MoveBCommand();
        moveLeft = new MoveLCommand();
        moveRight = new MoveRCommand();
        interact = new InteractCommand();

        rb = GetComponent<Rigidbody>();

        PrepBinds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepBinds()
    {
        defaultBind.Add(KeyCode.W, moveUp);
        defaultBind.Add(KeyCode.S, moveDown);
        defaultBind.Add(KeyCode.A, moveLeft);
        defaultBind.Add(KeyCode.D, moveRight);
        defaultBind.Add(KeyCode.E, interact);

        newBind.Add(KeyCode.W, moveUp);
        newBind.Add(KeyCode.S, moveDown);
        newBind.Add(KeyCode.A, moveLeft);
        newBind.Add(KeyCode.D, moveRight);
        newBind.Add(KeyCode.E, interact);
    }
}
