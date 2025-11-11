using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnwalkableObject : MonoBehaviour
{
    Pathfinding.RecastMeshObj recastMeshObj;
    private void Awake()
    {
        recastMeshObj = gameObject.AddComponent<Pathfinding.RecastMeshObj>();
        recastMeshObj.area = -1;
        recastMeshObj.dynamic = false;
        gameObject.layer = 9;

    }
}