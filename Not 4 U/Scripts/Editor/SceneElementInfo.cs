using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneElement))]
public class SceneElementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SceneElement sceneElement = (SceneElement)target;
        if(sceneElement.Info() != "")   EditorGUILayout.HelpBox(sceneElement.Info(), MessageType.Info);
        if(sceneElement.Warning() != "")   EditorGUILayout.HelpBox(sceneElement.Warning(), MessageType.Warning);
        DrawDefaultInspector();

    }
}

[CustomEditor(typeof(Filter))]
public class FilterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Filter filter = (Filter)target;
        if(filter.Info() != "")   EditorGUILayout.HelpBox(filter.Info(), MessageType.Info);
        if(filter.Warning() != "")   EditorGUILayout.HelpBox(filter.Warning(), MessageType.Warning);
        DrawDefaultInspector();

    }
}

// SCENE ELEMENTS

[CustomEditor(typeof(BeatSwitch))]
public class BeatSwitchEditor : SceneElementEditor{}

[CustomEditor(typeof(BoostChanger))]
public class BoostChangerEditor : SceneElementEditor{}

[CustomEditor(typeof(GravityChanger))]
public class GravityChangerEditor : SceneElementEditor{}

[CustomEditor(typeof(KartDetector))]
public class KartDetectorEditor : SceneElementEditor{}

[CustomEditor(typeof(KillZone))]
public class KillZoneEditor : SceneElementEditor{}

[CustomEditor(typeof(LapPoint))]
public class LapPointEditor : SceneElementEditor{}

[CustomEditor(typeof(RespawnOverride))]
public class RespawnOverrideEditor : SceneElementEditor{}

[CustomEditor(typeof(Switcher))]
public class SwitcherEditor : SceneElementEditor{}

[CustomEditor(typeof(VelocityChanger))]
public class VelocityChangerEditor : SceneElementEditor{}
[CustomEditor(typeof(ClingZone))]
public class ClingZoneEditor : SceneElementEditor{}
[CustomEditor(typeof(ClingVelocityZone))]
public class ClingVelocityZoneEditor : SceneElementEditor{}
[CustomEditor(typeof(Conveyor))]
public class ConveyorEditor : SceneElementEditor{}
[CustomEditor(typeof(TurnZone))]
public class TurnZoneEditor : SceneElementEditor{}

// FILTERS

[CustomEditor(typeof(SpeedFilter))]
public class SpeedFilterEditor : FilterEditor{}
[CustomEditor(typeof(SpeedPercentileFilter))]
public class SpeedPercentileFilterEditor : FilterEditor{}
[CustomEditor(typeof(TagFilter))]
public class TagFilterEditor : FilterEditor{}


[CustomEditor(typeof(PrefabRandomizer))]
public class PrefabRandomizerEditor : SceneElementEditor
{
    private SerializedProperty prefabsProp;
    private SerializedProperty placementPointProp;
    private SerializedProperty rotationProp;
    private SerializedProperty meshFilterProp;
    private SerializedProperty numberToPlaceProp;
    private SerializedProperty generatedObjectsProp;

    private void OnEnable()
    {
        // Cache the serialized properties
        prefabsProp = serializedObject.FindProperty("prefabs");
        placementPointProp = serializedObject.FindProperty("placementPoint");
        rotationProp = serializedObject.FindProperty("rotation");
        meshFilterProp = serializedObject.FindProperty("meshFilter");
        numberToPlaceProp = serializedObject.FindProperty("numberToPlace");
        generatedObjectsProp = serializedObject.FindProperty("generatedObjects");
    }
    public override void OnInspectorGUI()
    {
        SceneElement sceneElement = (SceneElement)target;
        if (sceneElement.Info() != "") EditorGUILayout.HelpBox(sceneElement.Info(), MessageType.Info);
        if (sceneElement.Warning() != "") EditorGUILayout.HelpBox(sceneElement.Warning(), MessageType.Warning);

        // Update the serialized object's representation
        serializedObject.Update();

        // Draw a custom header
        EditorGUILayout.LabelField("Setup", EditorStyles.boldLabel);

        // Display the prefab list
        EditorGUILayout.PropertyField(prefabsProp, new GUIContent("Prefabs"), true);

        // Display the placement point enum
        EditorGUILayout.PropertyField(placementPointProp, new GUIContent("Placement Point"));

        // Conditional display of the meshFilter based on the placementPoint
        if ((PrefabRandomizer.Placement)placementPointProp.enumValueIndex == PrefabRandomizer.Placement.AlongMesh)
        {
            EditorGUILayout.PropertyField(meshFilterProp, new GUIContent("Mesh Filter"));
        }

        // Display the placement point enum
        EditorGUILayout.PropertyField(rotationProp, new GUIContent("Rotation"));

        // Display the number to place
        EditorGUILayout.PropertyField(numberToPlaceProp, new GUIContent("Number To Place"));

        // Draw a separator for better visual distinction
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Results", EditorStyles.boldLabel);

        // Display the list of generated objects
        EditorGUILayout.PropertyField(generatedObjectsProp, new GUIContent("Generated Objects"), true);

        // Add buttons for generating and deleting objects
        PrefabRandomizer randomizer = (PrefabRandomizer)target;
        if (GUILayout.Button("Create Random"))
        {
            randomizer.CreateRandom();
        }
        if (GUILayout.Button("Delete Objects"))
        {
            randomizer.DeleteObjects();
        }

        // Apply any changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomEditor(typeof(Effector))]
public class EffectorEditor : SceneElementEditor
{
    public override void OnInspectorGUI()
    {
        Effector effector = (Effector)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Describe Functions"))
        {
            effector.DescribeFunctions();
        }

        // Apply any changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
