using UnityEngine;
using UnityEditor;
using System.IO;


public class AssetBundlesBuilder
{
    [MenuItem("Rising Front Tools/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        // Set the output paths
        string outputPathMac = "Assets/Mods/Stuff/Editor Components/Compiled Mods/Mac";
        string outputPathWin = "Assets/Mods/Stuff/Editor Components/Compiled Mods/Windows";

        // Create the directories if they don't exist
        Directory.CreateDirectory(outputPathMac);
        Directory.CreateDirectory(outputPathWin);

        // Build asset bundles for Mac
        BuildPipeline.BuildAssetBundles(outputPathMac, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
      //  BuildTarget.StandaloneOSXUniversal

        // Build asset bundles for Windows
        BuildPipeline.BuildAssetBundles(outputPathWin, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        // Move the built asset bundles to the desired location
        string moveAssetsPath = "Assets/Mod Content";
        MoveAssetBundles(outputPathMac, moveAssetsPath, "Mac");
        MoveAssetBundles(outputPathWin, moveAssetsPath, "Windows");
    }

    private static void MoveAssetBundles(string outputPath, string moveAssetsPath, string platform)
    {
        // Get the names of all asset bundles
        string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();

        // Move each asset bundle to a new folder named after it within the moveAssetsPath
        foreach (string bundleName in assetBundleNames)
        {
            string sourcePath = Path.Combine(outputPath, bundleName);
            if (File.Exists(sourcePath))
            {
                // Removing .workshopcontent from the directory name
                string cleanBundleName = bundleName.Replace(".workshopcontent", "");
                string destPathDir = Path.Combine(moveAssetsPath, cleanBundleName, platform);

                // Check if directory already exists, if not then create
                if (!Directory.Exists(destPathDir))
                {
                    Directory.CreateDirectory(destPathDir);
                }

                string destPath = Path.Combine(destPathDir, bundleName);

                // Check if file already exists at destination
                if (File.Exists(destPath))
                {
                    try
                    {
                        // Delete the existing file
                        File.Delete(destPath);
                        Debug.Log($"Deleted existing file at {destPath}.");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError($"Failed to delete existing file at {destPath}. Error: {e.Message}");
                        continue;
                    }
                }

                File.Move(sourcePath, destPath);
                Debug.Log($"Successfully moved file from {sourcePath} to {destPath}");

                // Handle the .manifest file
                string manifestSourcePath = sourcePath + ".manifest";
                string manifestDestPath = destPath + ".manifest";

                Debug.Log($"Attempting to move .manifest file from {manifestSourcePath} to {manifestDestPath}");

                // Check if manifest file exists
                if (File.Exists(manifestSourcePath))
                {
                    // Check if manifest file already exists at destination
                    if (File.Exists(manifestDestPath))
                    {
                        try
                        {
                            // Delete the existing manifest file
                            File.Delete(manifestDestPath);
                            Debug.Log($"Deleted existing manifest file at {manifestDestPath}.");
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError($"Failed to delete existing manifest file at {manifestDestPath}. Error: {e.Message}");
                            continue;
                        }
                    }

                    // Try to move the .manifest file
                    try
                    {
                        File.Move(manifestSourcePath, manifestDestPath);
                        Debug.Log($"Successfully moved .manifest file from {manifestSourcePath} to {manifestDestPath}");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError($"Failed to move manifest file from {manifestSourcePath} to {manifestDestPath}. Error: {e.Message}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Manifest file {manifestSourcePath} does not exist.");
                }
            }
        }
    }

}