using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Mounted Platoon", menuName = "Mounted Platoon")]
public class MountedPlatoonSO : ScriptableObject
{
    public int price = 100;
    public Sprite icon;
    public bool isGreenTeam = true;

    public MountedSoldierInfo[] mountedSoldiers;

    public string ID;
}
