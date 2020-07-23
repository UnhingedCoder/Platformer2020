using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(SaveManager))]
public class TestingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveManager myScript = (SaveManager)target;
        if (GUILayout.Button("Reset Game"))
        {
            myScript.ResetGame();
        }
    }
}
#endif
