using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        
    }

    public void Toggle()
    {
        if (gameObject.activeSelf == true) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
