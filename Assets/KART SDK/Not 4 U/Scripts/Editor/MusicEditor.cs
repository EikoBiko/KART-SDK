using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Music))]
public class MusicEditor : Editor
{
    private bool showLinks = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Extras", EditorStyles.boldLabel);

        showLinks = EditorGUILayout.Toggle("Show Useful Links", showLinks);

        if (showLinks)
        {
            
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.HelpBox("I designed this system to work with music at smashcustommusic.net; you can use the loop times listed for each song on there.\nHowever, you will need to use vgmsteam to convert them to usable file types.\nYou can use LoopingAudioConverter to make your own loops; just be sure to set \"Loop Options\" to \"Ask for all files\".", MessageType.Info);
            EditorGUILayout.LabelField("Useful Links", EditorStyles.boldLabel);

            if (GUILayout.Button("smashcustommusic.net"))
            {
                Application.OpenURL("https://smashcustommusic.net/");
            }

            if (GUILayout.Button("vgmstream"))
            {
                Application.OpenURL("https://github.com/vgmstream/vgmstream/releases/");
            }

            if (GUILayout.Button("Looping Audio Converter"))
            {
                Application.OpenURL("https://github.com/libertyernie/LoopingAudioConverter/releases/");
            }


            EditorGUILayout.EndVertical();
        }
    }
}
