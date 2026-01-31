using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] List<bool> listBools;

    bool isPaused = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeBool(int i)
    {
        if (i < listBools.Count) listBools[i] = !listBools[i];
    }

    public List<bool> GetBools()
    {
        return listBools;
    }

    public void DefaultBools()
    {
        for (int i = 0; i < listBools.Count; i++)
        {
            listBools[i] = false;
        }

        listBools[listBools.Count - 1] = true;
    }

    public void CallPause()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
    }
}
