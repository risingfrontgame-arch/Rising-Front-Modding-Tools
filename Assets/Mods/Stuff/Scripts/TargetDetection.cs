using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    public int targetPriority = 100;
    public bool isGreenTeam = false;
    public bool isArmored = true;
    public bool isAlive = true;
    public Transform targetTransform;
}