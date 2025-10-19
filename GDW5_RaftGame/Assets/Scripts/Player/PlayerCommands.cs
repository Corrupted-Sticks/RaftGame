using System.Collections.Generic;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    public static PlayerCommands PCInstance;

    private PCommand moveUp, moveDown, moveLeft, moveRight, interact;

    Dictionary<KeyCode, PCommand> defaultBind = new();
    Dictionary<KeyCode, PCommand> newBind = new();

    KeyCode key;
    KeyCode keyForward = KeyCode.W;
    KeyCode keyBackward = KeyCode.S;
    KeyCode keyLeft = KeyCode.A;
    KeyCode keyRight = KeyCode.D;
    KeyCode keyInteract = KeyCode.E;

    private void Awake()
    {
        if (PCInstance != null && PCInstance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveUp = new MoveFCommand();
        moveDown = new MoveBCommand();
        moveLeft = new MoveLCommand();
        moveRight = new MoveRCommand();
        interact = new InteractCommand();

        PrepBinds();
    }

    void PrepBinds()
    {
        defaultBind.Add(KeyCode.W, moveUp);
        defaultBind.Add(KeyCode.S, moveDown);
        defaultBind.Add(KeyCode.A, moveLeft);
        defaultBind.Add(KeyCode.D, moveRight);
        defaultBind.Add(KeyCode.E, interact);

        newBind.Add(keyForward, moveUp);
        newBind.Add(keyBackward, moveDown);
        newBind.Add(keyLeft, moveLeft);
        newBind.Add(keyRight, moveRight);
        newBind.Add(keyInteract, interact);
    }

    public void SelectBind(string action)
    {
        switch (action)
        {
            case "forward":
                newBind.Remove(keyForward);

                keyForward = GetNewBind();

                newBind.Add(keyForward, moveUp);
                break;
            case "backward":
                break;
            case "left":
                break;
            case "right":
                break;
            case "interact":
                break;
            default:
                break;
        }
    }

    KeyCode GetNewBind()
    {
        while (!Input.anyKeyDown)
        {

        }

        KeyCode newKey = KeyCode.W;

        return newKey;
    }
}
