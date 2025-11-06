using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    Canvas canvas;
    private void Awake()
    {
        if(instance != null) Destroy(instance.gameObject);
        instance = this;
        canvas = GetComponentInParent<Canvas>();
    }


    public bool isShown { get => canvas.enabled = false; }
    public void Hide()
    {
        canvas.enabled = false;
    }
    public void Show()
    {
        canvas.enabled = true;
    }
    public void Toggle()
    {
        if (canvas.enabled) Hide();
        else Show();
    }
}
