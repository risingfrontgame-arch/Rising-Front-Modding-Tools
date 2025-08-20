using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor;

[ExecuteInEditMode]
public class DeployableObject: MonoBehaviour
{
    public int buildableID = 9999;
    [HideInInspector]
    public Pathfinding.RecastTileUpdate tileUpdates;
    public StaticEmplacement staticEmplacement;
    
    public Collider[] cols;
  
    public GameObject[] disableObjs;
    public GameObject[] showTheseObjectsWhileItsBeingPlaced;
    public FlareLauncher flare;
    public float heightOffset = 0f;
    [HideInInspector]
    public float distanceOffset = 2f;
    public float health = 100f;

    [Header("Auto Destruction Settings")]
    public bool autoDestroyGameObject = false;
    public float autoDestroyDelay = 100f;
    //  public CommandManager cmdManager;
    // public GameReference game;
    bool objPlaced = false;
    [HideInInspector]
    public int deployID = 0;
    [HideInInspector]
   public GameObject navUpdater;
    public bool ScanTopForCoverPositions = false;

   

    private void Reset()
    {
        if (buildableID == 9999)
        {
            
            Random.InitState( (int)System.DateTime.Now.Ticks);
          
            buildableID = Random.Range(-999999999, 999999999);
        }
    }

    private void Update()
    {
        cols = GetComponentsInChildren<Collider>();
        if(tileUpdates == null)
        {
            tileUpdates = GetComponent<RecastTileUpdate>();
            if(tileUpdates == null)
            {
                tileUpdates = gameObject.AddComponent<RecastTileUpdate>();
            }
        }
        if(staticEmplacement == null)
        {
            staticEmplacement = GetComponent<StaticEmplacement>();
        }
        if (staticEmplacement == null) {
            Transform[] transforms = GetComponentsInChildren<Transform>();
            foreach(Transform trans in transforms)
            {
                if (ScanTopForCoverPositions == false)
                {
                    trans.gameObject.layer = 21;
                } else
                {
                    trans.gameObject.layer = 15;
                }
               
            }
           
        }
    }

}
