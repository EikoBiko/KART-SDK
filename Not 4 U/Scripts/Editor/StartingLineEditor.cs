using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(StartingLine))]
public class StartingLineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        StartingLine startingLine = (StartingLine)target;
        if (GUILayout.Button("Locate Ground"))
        {
            startingLine.GroundPositions();
        }
    }
}
