using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoDeployableSOChecker : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            // Check if the imported asset is a DeployableSO ScriptableObject
            if (str.EndsWith(".asset"))
            {
                DeployableSO deployable = AssetDatabase.LoadAssetAtPath<DeployableSO>(str);

                if (deployable != null)
                {
                    CheckDeployableSO();
                    break; // Exit the loop after checking, no need to continue looking
                }
            }
        }
    }

    private static void CheckDeployableSO()
    {
        // Find all DeployableSO ScriptableObjects
        string[] guids = AssetDatabase.FindAssets("t:DeployableSO");
        Dictionary<int, DeployableSO> deployableMap = new Dictionary<int, DeployableSO>();

        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            DeployableSO deployable = AssetDatabase.LoadAssetAtPath<DeployableSO>(assetPath);

            if (deployableMap.ContainsKey(deployable.ID))
            {
                // Found a duplicate ID
                Debug.LogWarning($"Duplicate ID {deployable.ID} found for DeployableSO: {assetPath}");

                // Compare the asset modification date and call Initialize() on the newest asset
                string existingAssetPath = AssetDatabase.GetAssetPath(deployableMap[deployable.ID]);
                System.DateTime existingAssetTime = System.IO.File.GetLastWriteTime(existingAssetPath);
                System.DateTime currentAssetTime = System.IO.File.GetLastWriteTime(assetPath);

                if (existingAssetTime < currentAssetTime)
                {
                    deployable.Initialize();
                }
                else
                {
                    deployableMap[deployable.ID].Initialize();
                }
            }
            else
            {
                deployableMap.Add(deployable.ID, deployable);
            }
        }

        Debug.Log("Finished checking DeployableSO IDs.");
    }
}

