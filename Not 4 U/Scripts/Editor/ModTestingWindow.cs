using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Linq;

public class ModTestingWindow : EditorWindow
{
    private string demoPath = "";
    private string assetBundlePath = "";
    
    private AssetBundle loadedBundle = null;
    private Mod loadedMod = null;

    private bool includeTrack = false;
    private bool includeRacer = false;
    private bool includeVehicle = false;

    private int trackIndex = 0;
    private int racerIndex = 0;
    private int vehicleIndex = 0;

    [MenuItem("KART SDK/Playtest...")]
    public static void ShowWindow()
    {
        GetWindow<ModTestingWindow>("Mod Testing");
    }

    private const string DemoPathKey = "ModTestEditor_DemoPath";
    private const string AssetBundlePathKey = "ModTestEditor_AssetBundlePath";

    private void OnEnable()
    {
        // Load stored paths when the window opens
        demoPath = EditorPrefs.GetString(DemoPathKey, "");
        assetBundlePath = EditorPrefs.GetString(AssetBundlePathKey, "");
    }

    private void SavePaths()
    {
        EditorPrefs.SetString(DemoPathKey, demoPath);
        EditorPrefs.SetString(AssetBundlePathKey, assetBundlePath);
    }

    void OnGUI()
    {
        GUILayout.Label("Configure Mod Testing", EditorStyles.boldLabel);

        // Demo Path
        GUILayout.Label("Application Path", EditorStyles.label);
        demoPath = EditorGUILayout.TextField(demoPath);

        if (GUILayout.Button("Select KART Path..."))
        {
            string initialDirectory = string.IsNullOrEmpty(demoPath) ? "" : System.IO.Path.GetDirectoryName(demoPath);
            string newPath = EditorUtility.OpenFilePanel("Where's your KART.exe?", initialDirectory, "exe");
            if (!string.IsNullOrEmpty(newPath))
            {
                demoPath = newPath;
                SavePaths(); // Save the path
            }
        }

        // AssetBundle Path
        GUILayout.Label("AssetBundle Path", EditorStyles.label);
        assetBundlePath = EditorGUILayout.TextField(assetBundlePath);

        if (GUILayout.Button("Select mod..."))
        {
            string initialDirectory = string.IsNullOrEmpty(assetBundlePath) ? "" : System.IO.Path.GetDirectoryName(assetBundlePath);
            string newPath = EditorUtility.OpenFilePanel("Pick a .kartstuff file!", initialDirectory, "kartstuff");
            if (!string.IsNullOrEmpty(newPath))
            {
                assetBundlePath = newPath;
                SavePaths(); // Save the path
            }
        }

        GUILayout.Space(10);

        if(demoPath.EndsWith(".exe") && assetBundlePath.EndsWith(".kartstuff")){
            if(GUILayout.Button("Click to load " + Path.GetFileName(assetBundlePath))){
                LoadAssetBundle();
            }
        }

        GUILayout.Space(10);

        if (loadedMod != null)
        {
            GUILayout.Label("I want to...", EditorStyles.boldLabel);

            if (loadedMod.tracks.Count > 0)
            {
                // Tracks
                includeTrack = EditorGUILayout.Toggle("Test a track", includeTrack);
                if (includeTrack && loadedMod.tracks != null)
                {
                    trackIndex = DisplayRadioButtonList(loadedMod.tracks, trackIndex);
                    GUILayout.Space(10);
                }
            }else{
                includeTrack = false;
            }

            if (loadedMod.racers.Count > 0)
            {
                // Racers
                includeRacer = EditorGUILayout.Toggle("Test a racer", includeRacer);
                if (includeRacer && loadedMod.racers != null)
                {
                    racerIndex = DisplayRadioButtonList(loadedMod.racers, racerIndex);
                    GUILayout.Space(10);
                }
            }else{
                includeRacer = false;
            }

            if (loadedMod.vehicles.Count > 0)
            {
                // Vehicles
                includeVehicle = EditorGUILayout.Toggle("Test a vehicle", includeVehicle);
                if (includeVehicle && loadedMod.vehicles != null)
                {
                    vehicleIndex = DisplayRadioButtonList(loadedMod.vehicles, vehicleIndex);
                    GUILayout.Space(10);
                }
            }else{
                includeVehicle = false;
            }
        }



        if((!includeRacer && !includeVehicle && !includeTrack) || loadedMod == null){
            return;
        }

        if (includeTrack || includeVehicle || includeRacer)
        {
            GUILayout.Space(10);
            // Create a box for the track preview section
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(10, 10, 10, 10), // Padding inside the box
                margin = new RectOffset(10, 10, 10, 10)  // Margin outside the box
            };

            GUILayout.Label("TEST OVERVIEW", EditorStyles.boldLabel);

            GUILayout.BeginVertical(boxStyle);

            if (includeTrack && trackIndex < loadedMod.tracks.Count)
            {
                var selectedTrack = loadedMod.tracks[trackIndex].track;
                GUILayout.Label("Track: " + selectedTrack.trackTitle, EditorStyles.boldLabel);

                // Display Track Preview (Texture2D)
                if (selectedTrack.trackPreview != null)
                {
                    GUILayout.Label(selectedTrack.trackPreview, GUILayout.Width(192), GUILayout.Height(108));
                }
                else
                {
                    GUILayout.Label("No preview available", GUILayout.Width(192), GUILayout.Height(108));
                }
                GUILayout.Space(10);
            }
            if (includeRacer && racerIndex < loadedMod.racers.Count)
            {
                var selectedRacer = loadedMod.racers[racerIndex].racer;
                GUILayout.Label("Racer: " + selectedRacer.racerName, EditorStyles.boldLabel);
                Texture2D thumbnail = AssetPreview.GetAssetPreview(selectedRacer.racerPrefab);
                if(thumbnail != null){
                    GUILayout.Label(thumbnail, GUILayout.Width(64), GUILayout.Height(64));
                }
                GUILayout.Space(10);
            }
            if (includeVehicle && vehicleIndex < loadedMod.racers.Count)
            {
                var selectedVehicle = loadedMod.vehicles[vehicleIndex].vehicle;
                GUILayout.Label("Vehicle: " + selectedVehicle.vehicleName, EditorStyles.boldLabel);
                Texture2D thumbnail = AssetPreview.GetAssetPreview(selectedVehicle.vehiclePrefab);
                if(thumbnail != null){
                    GUILayout.Label(thumbnail, GUILayout.Width(64), GUILayout.Height(64));
                }
                GUILayout.Space(10);
            }

            // Launch Button
            if (GUILayout.Button("Test!"))
            {
                if (!string.IsNullOrEmpty(demoPath) && !string.IsNullOrEmpty(assetBundlePath))
                {
                    LaunchDemoWithAssetBundle();
                }
                else
                {
                    EditorUtility.DisplayDialog("Nice try!", "Please set both the demo path AND the AssetBundle path.", "Fine...");
                }
            }

            GUILayout.EndVertical(); // End of the box
        }
    }

    void LoadAssetBundle()
    {
        if (loadedBundle != null)
        {
            loadedBundle.Unload(false);
            loadedBundle = null;
            loadedMod = null;
            EditorUtility.DisplayDialog("Woah!", "An asset bundle is already loaded.", "So?");
        }

        if (!string.IsNullOrEmpty(assetBundlePath) && File.Exists(assetBundlePath))
        {
            loadedBundle = AssetBundle.LoadFromFile(assetBundlePath);

            if (loadedBundle != null)
            {
                // Retrieve all assets in the AssetBundle
                var allAssets = loadedBundle.LoadAllAssets<ScriptableObject>();
                
                // Check if there is more than one root object in the AssetBundle
                if (allAssets.Length != 1)
                {
                    EditorUtility.DisplayDialog("Woops!", "The AssetBundle should contain exactly one Mod, and nothing else. We found this many things: " + allAssets.Length, "Alright then!");
                    loadedBundle.Unload(false);
                    loadedBundle = null;
                    return;
                }

                // Check if the root object is of type Mod
                var rootObject = allAssets.FirstOrDefault();
                if (rootObject is Mod mod)
                {
                    loadedMod = mod;
                }
                else
                {
                    EditorUtility.DisplayDialog("Uh-oh!", "The root object in the AssetBundle is not a Mod. Looks like it's a " + rootObject.GetType().Name, "Oh...");
                }
                loadedBundle.Unload(false);
                loadedBundle = null;
            }
            else
            {
                EditorUtility.DisplayDialog("???", "Failed to load AssetBundle, but I'm not sure why! You're on your own, buddy.", "Thanks for nothing!");
            }
        }
    }


    int DisplayRadioButtonList<T>(System.Collections.Generic.List<T> list, int selectedIndex)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (GUILayout.Toggle(selectedIndex == i, list[i].ToString()))
            {
                selectedIndex = i;
            }
        }
        return selectedIndex;
    }

    void LaunchDemoWithAssetBundle()
    {
        ProcessStartInfo processInfo = new ProcessStartInfo();
        processInfo.FileName = demoPath;

        // Construct the arguments
        string arguments = $"--modtest \"{assetBundlePath}\"";
        
        if (includeTrack) arguments += $" --track {trackIndex}";
        if (includeRacer) arguments += $" --racer {racerIndex}";
        if (includeVehicle) arguments += $" --vehicle {vehicleIndex}";

        processInfo.Arguments = arguments;
        processInfo.UseShellExecute = true;

        try
        {
            Process.Start(processInfo);
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to launch the demo.\n{e.Message}", "OK");
        }
    }
}
