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
            textBoxes[i].text = PlayerCommands.PCInstance.GetTextBoxString(i);
        }
    }
}
