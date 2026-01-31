using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    Canvas canvas;

    private void Awake()
    {
        instance = this;
        canvas = GetComponent<Canvas>();
    }

    public void ToggleUI()
    {
        canvas.enabled = !canvas.enabled;
    }

    public void Resume()
    {
        ToggleUI();
        GameManager.instance.CallPause();
    }
}
