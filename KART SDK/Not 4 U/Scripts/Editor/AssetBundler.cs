using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class AssetBundler : Editor
{
    [MenuItem("KART SDK/Export Mod(s) to .kartstuff")]
    private static void BuildAssetBundles()
    {
        // Specify the output directory for Asset Bundles
        string outputDirectory = "Assets/KART SDK/AssetBundles";

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Find all Mod assets in the AssetDatabase
        string[] assetPaths = AssetDatabase.FindAssets("t:Mod")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .ToArray();

        foreach (string assetPath in assetPaths)
        {
            // Load the Mod asset
            Mod mod = AssetDatabase.LoadAssetAtPath<Mod>(assetPath);

            if (mod != null)
            {
                if(!mod.CheckModValidity()){
                    mod.GiveError(mod.name + " will not export! See above issue.", mod);
                    continue;
                }
                // Run pre-bundling functions on the Mod
                PreBundleMod(mod);

                // Build AssetBundle for the Mod
                BuildAssetBundleForMod(mod, assetPath, outputDirectory);
            }
        }

        // Rename asset bundle files with the desired extension
        RenameAssetBundleFiles(outputDirectory);

        Debug.Log("AssetBundles exported to: " + outputDirectory);
    }

    private static void PreBundleMod(Mod mod)
    {
        foreach(UnlockableTrack unlockable in mod.tracks){
            GenerateUnlockableIDs(unlockable);
        }
        foreach(UnlockableRacer unlockable in mod.racers){
            GenerateUnlockableIDs(unlockable);
        }
        foreach(UnlockableVehicle unlockable in mod.vehicles){
            GenerateUnlockableIDs(unlockable);
        }
        foreach(UnlockableMusic unlockable in mod.music){
            GenerateUnlockableIDs(unlockable);
        }
        foreach(UnlockableHorn unlockable in mod.horns){
            GenerateUnlockableIDs(unlockable);
        }
    }

    private static void GenerateUnlockableIDs(Unlockable unlockable){
        Hash128 zeroHash = new Hash128();
        if(unlockable.itemID == zeroHash){
            unlockable.GenerateID();
        }
    }

    private static void BuildAssetBundleForMod(Mod mod, string assetPath, string outputDirectory)
    {
        // Define AssetBundle name based on Mod's path or name
        string bundleName = Path.GetFileNameWithoutExtension(assetPath) + ".kartstuff";

        // Build AssetBundle with only the Mod asset
        AssetBundleBuild[] buildMap = new AssetBundleBuild[]
        {
            new AssetBundleBuild
            {
                assetBundleName = bundleName,
                assetNames = new string[] { assetPath }
            }
        };

        BuildPipeline.BuildAssetBundles(outputDirectory, buildMap, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }

    private static void RenameAssetBundleFiles(string outputDirectory)
    {
        string[] assetBundleFiles = Directory.GetFiles(outputDirectory);
        foreach (string filePath in assetBundleFiles)
        {
            // Check if the file doesn't have an extension and is not the manifest file
            if (Path.GetExtension(filePath) == "" && Path.GetFileNameWithoutExtension(filePath) != "AssetBundles")
            {
                // Set new file path with the desired extension
                string newFilePath = filePath + ".kartstuff";

                // If a file with the new path already exists, delete it to avoid conflicts
                if (File.Exists(newFilePath))
                {
                    File.Delete(newFilePath);
                }

                // Rename the file
                File.Move(filePath, newFilePath);
            }
        }
    }
}
