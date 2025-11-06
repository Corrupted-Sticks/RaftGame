using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string str)
    {
        SceneManager.LoadScene(str);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR // do not remove this.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
