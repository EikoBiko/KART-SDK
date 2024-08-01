using UnityEditor;
using UnityEngine;

public class AssetBundler : Editor
{
    [MenuItem("KART SDK/Build Asset Bundles")]
    private static void BuildAssetBundles()
    {
        // Specify the output directory for Asset Bundles
        string outputDirectory = "Assets/KART SDK/AssetBundles";

        if (!System.IO.Directory.Exists(outputDirectory))
        {
            System.IO.Directory.CreateDirectory(outputDirectory);
        }

        // Build options for AssetBundles
        BuildAssetBundleOptions options = BuildAssetBundleOptions.None;

        BuildPipeline.BuildAssetBundles(outputDirectory, options, EditorUserBuildSettings.activeBuildTarget);

        // Rename asset bundle files with the desired extension
        string[] assetBundleFiles = System.IO.Directory.GetFiles(outputDirectory);
        foreach (string filePath in assetBundleFiles)
        {
            // Check if the file doesn't have an extension and is not the manifest file
            if (System.IO.Path.GetExtension(filePath) == "" && System.IO.Path.GetFileNameWithoutExtension(filePath) != "AssetBundles")
            {
                // Rename the file with the desired extension
                string newFilePath = filePath + ".kartstuff";
                System.IO.File.Move(filePath, newFilePath);
            }
        }

        Debug.Log("AssetBundles exported to: " + outputDirectory);
    }

}
