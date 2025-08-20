using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SetReadWrite : MonoBehaviour
{
    [MenuItem("Rising Front Tools/Optimize Mods (Click this before you Build Assetbundles)")]
    static void SetReadWriteForBuildables()
    {
        // Set Read/Write Enabled to OFF and Mesh Compression to Medium for all .fbx, .obj, and .blend models in the "!" folder
        string[] allModelGuids = AssetDatabase.FindAssets("t:Model");

        foreach (string guid in allModelGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.EndsWith(".fbx") || path.EndsWith(".obj") || path.EndsWith(".blend"))
            {
                ModelImporter modelImporter = AssetImporter.GetAtPath(path) as ModelImporter;
                if (modelImporter != null)
                {
                    modelImporter.isReadable = false;
                    modelImporter.meshCompression = ModelImporterMeshCompression.Medium;
                    modelImporter.SaveAndReimport();
                    Debug.Log("Disabled Read/Write and set Mesh Compression to Medium for: " + path);
                }
            }
        }

        // Get all DeployableSO assets in the project
        string[] guids = AssetDatabase.FindAssets("t:DeployableSO");
        List<string> processedPaths = new List<string>();
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            DeployableSO deployable = AssetDatabase.LoadAssetAtPath<DeployableSO>(path);

            if (deployable != null && deployable.objPrefab != null)
            {
                // Find all MeshFilters in the prefab
                MeshFilter[] meshFilters = deployable.objPrefab.GetComponentsInChildren<MeshFilter>();

                foreach (MeshFilter meshFilter in meshFilters)
                {
                    if (meshFilter.sharedMesh != null)
                    {
                        string assetPath = AssetDatabase.GetAssetPath(meshFilter.sharedMesh);

                        if (!string.IsNullOrEmpty(assetPath) && !processedPaths.Contains(assetPath))
                        {
                            ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                            if (modelImporter != null)
                            {
                                modelImporter.isReadable = true;
                                modelImporter.SaveAndReimport();
                                processedPaths.Add(assetPath);

                                Debug.Log("Enabled Read/Write for: " + assetPath);
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("Finished setting Read/Write Enabled and Mesh Compression for deployable meshes.");
    }
}