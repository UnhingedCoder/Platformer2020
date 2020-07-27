using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class TestingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;
        if (GUILayout.Button("Reset Game"))
        {
            myScript.ResetGame();
        }
    }
}
#endif
