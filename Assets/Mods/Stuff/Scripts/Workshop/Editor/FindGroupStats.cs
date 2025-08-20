
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class FindGroupStats : MonoBehaviour
{

    private void Update()
    {
        print("called");
        FindAllGroupStats();
    }
  
    public void FindAllGroupStats()
    {
        string[] guids = AssetDatabase.FindAssets("t:GroupStats");  // the 't:' means filter by type
        GroupStats[] groupStatsArray = new GroupStats[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            GroupStats asset = AssetDatabase.LoadAssetAtPath<GroupStats>(assetPath);
            groupStatsArray[i] = asset;
        }

        // Now groupStatsArray contains all GroupStats ScriptableObjects in the project
        foreach (GroupStats stats in groupStatsArray)
        {
            Debug.Log(stats.name);
            //stats.InitializeID();
        }
    }
}

