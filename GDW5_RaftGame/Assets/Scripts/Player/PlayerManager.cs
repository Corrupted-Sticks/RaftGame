using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cosmetics;

    private void Awake()
    {
        try
        {
            ToggleCosmetics(GameManager.instance.GetBools());
        }
        catch (Exception e)
        {
            Debug.Log("Haven't loaded GameManager yet");
        }
    }

    public void ToggleCosmetics(List<bool> listBools)
    {
        for (int i = 0; i < listBools.Count; i++)
        {
            if (listBools[i])
            {
                cosmetics[i].SetActive(true);
            }
            else
            {
                cosmetics[i].SetActive(false);
            }
        }
    }
}
