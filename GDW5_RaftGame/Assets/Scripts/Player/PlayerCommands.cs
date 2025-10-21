using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerCommands : MonoBehaviour
{
    public static PlayerCommands PCInstance;

    private PCommand moveUp, moveDown, moveLeft, moveRight, interact;

    KeyCode key;

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

    public List<string> texts = new List<string>();

    bool needInput = false;
    string actionInput;

    private void Awake()
    {
        if (PCInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            PCInstance = this;
            DontDestroyOnLoad(gameObject);
        }
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
            }
        }
    }

    void PrepBinds()
    {
        for (int i = 0; i < defaultBinds.Count; i++)
        {
            if (keyBinds.Count < defaultBinds.Count)
            {
                keyBinds.Add(defaultBinds[i]);
                texts.Add(defaultBinds[i].ToString());
            }
            else
            {
                keyBinds[i] = defaultBinds[i];
                texts[i] = defaultBinds[i].ToString();
            }
        }
    }

    public void Rebind(string action)
    {
        if (!needInput)
        {
            actionInput = action;
            needInput = true;
        }
    }

    void SetNewBind()
    {
        switch (actionInput)
        {
            case "forward":
                keyBinds[0] = key;
                texts[0] = key.ToString();
                break;
            case "backward":
                keyBinds[1] = key;
                texts[1] = key.ToString();
                break;
            case "left":
                keyBinds[2] = key;
                texts[2] = key.ToString();
                break;
            case "right":
                keyBinds[3] = key;
                texts[3] = key.ToString();
                break;
            case "interact":
                keyBinds[4] = key;
                texts[4] = key.ToString();
                break;
            default:
                break;
        }
    }

    public void SetDefault()
    {
        PrepBinds();
    }

    public string GetTextBoxString(int num)
    {
        return texts[num];
    }
}
