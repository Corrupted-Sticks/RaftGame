using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    public static PlayerCommands PCInstance;

    private PCommand moveUp, moveDown, moveLeft, moveRight, interact;

    //Dictionary<KeyCode, PCommand> oldDefaultBind = new();
    //public Dictionary<KeyCode, PCommand> newBind { get; private set; } = new();

    KeyCode key;
    /*public KeyCode keyForward { get; private set; } = KeyCode.W;
    public KeyCode keyBackward { get; private set; } = KeyCode.S;
    public KeyCode keyLeft { get; private set; } = KeyCode.A;
    public KeyCode keyRight { get; private set; } = KeyCode.D;
    public KeyCode keyInteract { get; private set; } = KeyCode.E;*/

    List<KeyCode> defaultBinds = new List<KeyCode>()
    {
        KeyCode.W,
        KeyCode.S,
        KeyCode.A,
        KeyCode.D,
        KeyCode.E
    };

    public List<KeyCode> keyBinds { get; private set; } = new List<KeyCode>();

    public List<PCommand> keyCommands { get; private set; } = new List<PCommand>();

    bool needInput = false;
    string actionInput;

    private void Awake()
    {
        if (PCInstance != null && PCInstance != this)
        {
            Destroy(this);
        }

        PCInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        moveUp = new MoveFCommand();
        moveDown = new MoveBCommand();
        moveLeft = new MoveLCommand();
        moveRight = new MoveRCommand();
        interact = new InteractCommand();

        keyCommands.Add(moveUp);
        keyCommands.Add(moveDown);
        keyCommands.Add(moveLeft);
        keyCommands.Add(moveRight);
        keyCommands.Add(interact);

        //PrepDefaults();
        PrepBinds();
    }

    private void Update()
    {
        if (needInput)
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keycode) && !Input.GetKey(KeyCode.Escape) && !keyBinds.Contains(keycode))
                {
                    key = keycode;

                    SetNewBind();
                    needInput = false;
                }
                else
                {
                    needInput = false;
                    break;
                }
            }
        }
    }

    /*void PrepDefaults()
    {
        oldDefaultBind.Add(KeyCode.W, moveUp);
        oldDefaultBind.Add(KeyCode.S, moveDown);
        oldDefaultBind.Add(KeyCode.A, moveLeft);
        oldDefaultBind.Add(KeyCode.D, moveRight);
        oldDefaultBind.Add(KeyCode.E, interact);
    }*/

    void PrepBinds()
    {
        //int j = 0;

        for (int i = 0; i < defaultBinds.Count; i++)
        {
            if (keyBinds.Count < defaultBinds.Count)
            {
                keyBinds.Add(defaultBinds[i]);
            }
            else
            {
                keyBinds[i] = defaultBinds[i];
            }
        }

        /*foreach(KeyValuePair<KeyCode, PCommand> item in oldDefaultBind)
        {
            newBind.Add(item.Key, item.Value);
            keyBinds[j] = item.Key;
            j++;
        }*/
    }

    public void Rebind(string action)
    {
        actionInput = action;
        needInput = true;
    }

    void SetNewBind()
    {
        switch (actionInput)
        {
            case "forward":
                //newBind.Remove(keyBinds[0]);
                keyBinds[0] = key;
                //newBind.Add(keyBinds[0], moveUp);
                break;
            case "backward":
                //newBind.Remove(keyBinds[1]);
                keyBinds[1] = key;
                //newBind.Add(keyBinds[1], moveDown);
                break;
            case "left":
                //newBind.Remove(keyBinds[2]);
                keyBinds[2] = key;
                //newBind.Add(keyBinds[2], moveLeft);
                break;
            case "right":
                //newBind.Remove(keyBinds[3]);
                keyBinds[3] = key;
                //newBind.Add(keyBinds[3], moveRight);
                break;
            case "interact":
                //newBind.Remove(keyBinds[4]);
                keyBinds[4] = key;
                //newBind.Add(keyBinds[4], interact);
                break;
            case "default":
                //newBind.Clear();
                PrepBinds();
                break;
            default:
                break;
        }
    }
}
