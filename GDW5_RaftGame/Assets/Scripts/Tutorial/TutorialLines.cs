using UnityEngine;

[CreateAssetMenu(menuName ="Same Day Shipping/Create Tutorial Dialogue Object", fileName ="TutorialLines")]
public class TutorialLines : ScriptableObject
{
    public int curLine = 0;
    [TextArea(3,6)]
    [SerializeField] string[] lines;

    public string GetLine(int index) => index<lines.Length ? lines[index] : " "; // if index in range, return line, otherwise return empty string.
    
}
