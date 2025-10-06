using UnityEngine;

public class SceneSender : MonoBehaviour
{
    public void SendScene(string str)
    {
        SceneLoader.Instance.LoadScene(str);
    }

    public void SendQuit()
    {
        SceneLoader.Instance.QuitGame();
    }
}
