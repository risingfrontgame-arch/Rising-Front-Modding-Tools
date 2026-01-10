using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    public int targetPriority = 100;
    public bool isGreenTeam = false;
    [Tooltip("Set to false to be targeted by small arms fire")]
    public bool isArmored = true;
    public bool isAlive = true;
    public Transform targetTransform;
}