using UnityEngine;

public class CustomHandler : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.DefaultBools();
    }

    public void GoChange(int i)
    {
        GameManager.instance.ChangeBool(i);
    }
}
