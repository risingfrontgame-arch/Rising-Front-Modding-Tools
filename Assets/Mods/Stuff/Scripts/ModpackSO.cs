using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Mod Pack", menuName = "Create New Mod Pack")]
public class ModpackSO : ScriptableObject
{

    public DeployableSO[] buildables;
    public GroupStats[] platoons;
    public MountedPlatoonSO[] mountedPlatoons;
    public PlayerItem[] playerItems;
    public VehicleSO[] vehicles;
    public Deployables[] fireSupport;



}