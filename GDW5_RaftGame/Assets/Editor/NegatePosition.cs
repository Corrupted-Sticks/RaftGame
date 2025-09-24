using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class NegatePositionWindow : EditorWindow
{
    private bool localSpace = true;
    private bool negateX = true;
    private bool negateY = false;
    private bool negateZ = false;

    [MenuItem("Tools/Position Negator")]
    public static void ShowWindow()
    {
        GetWindow<NegatePositionWindow>("Position Negator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Negate Transform Position", EditorStyles.boldLabel);

        localSpace = EditorGUILayout.Toggle("Local space?", localSpace);
        EditorGUILayout.Space();
        negateX = EditorGUILayout.Toggle("Negate X", negateX);
        negateY = EditorGUILayout.Toggle("Negate Y", negateY);
        negateZ = EditorGUILayout.Toggle("Negate Z", negateZ);

        EditorGUILayout.Space();

        if (GUILayout.Button("Apply to Selected"))
        {
            NegateSelectedPositions();
        }
    }

    private void NegateSelectedPositions()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Undo.RecordObject(go.transform, "Negate Transform Position");

            Vector3 pos;
            if (localSpace) pos = go.transform.localPosition;
            else pos = go.transform.position;


            if (negateX) pos.x = -pos.x;
            if (negateY) pos.y = -pos.y;
            if (negateZ) pos.z = -pos.z;

            if(localSpace) go.transform.localPosition = pos;
            else go.transform.position = pos;
        }
    }
}
