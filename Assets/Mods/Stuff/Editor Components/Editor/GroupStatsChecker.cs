//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class AutoGroupStatsChecker : AssetPostprocessor
//{
//    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
//    {
//        foreach (string str in importedAssets)
//        {
//            // Check if the imported asset is a GroupStats ScriptableObject
//            if (str.EndsWith(".asset"))
//            {
//                GroupStats stats = AssetDatabase.LoadAssetAtPath<GroupStats>(str);

//                if (stats != null)
//                {
//                    CheckGroupStats();
//                    break; // Exit the loop after checking, no need to continue looking
//                }
//            }
//        }
//    }

//    private static void CheckGroupStats()
//    {
//        // Find all GroupStats ScriptableObjects
//        string[] guids = AssetDatabase.FindAssets("t:GroupStats");
//        Dictionary<int, GroupStats> groupStatsMap = new Dictionary<int, GroupStats>();

//        foreach (var guid in guids)
//        {
//            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
//            GroupStats stats = AssetDatabase.LoadAssetAtPath<GroupStats>(assetPath);

//            if (groupStatsMap.ContainsKey(stats.ID))
//            {
//                // Found a duplicate ID
//                Debug.LogWarning($"Duplicate ID {stats.ID} found for GroupStats: {assetPath}");

//                // Compare the asset modification date and call Initialize() on the newest asset
//                string existingAssetPath = AssetDatabase.GetAssetPath(groupStatsMap[stats.ID]);
//                System.DateTime existingAssetTime = System.IO.File.GetLastWriteTime(existingAssetPath);
//                System.DateTime currentAssetTime = System.IO.File.GetLastWriteTime(assetPath);

//                if (existingAssetTime < currentAssetTime)
//                {
//                    stats.Initialize();
//                }
//                else
//                {
//                    groupStatsMap[stats.ID].Initialize();
//                }
//            }
//            else
//            {
//                groupStatsMap.Add(stats.ID, stats);
//            }
//        }

//        Debug.Log("Finished checking GroupStats IDs.");
//    }
//}