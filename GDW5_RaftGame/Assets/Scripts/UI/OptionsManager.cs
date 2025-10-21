using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> textBoxes = new List<TextMeshProUGUI>();

    private void Update()
    {
        for (int i = 0; i < textBoxes.Count; i++)
        {
            //textBoxes[i].text = PlayerCommands.PCInstance.GetTextBoxString(i);
            textBoxes[i].text = PlayerCommands.PCInstance.texts[i];
        }
    }

    public void GoRebind(string action)
    {
        PlayerCommands.PCInstance.Rebind(action);
    }

    public void GoDefault()
    {
        PlayerCommands.PCInstance.SetDefault();
    }
}
