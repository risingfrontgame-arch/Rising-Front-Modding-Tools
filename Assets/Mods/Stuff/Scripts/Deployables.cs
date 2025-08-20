using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Deployable", menuName = "Deployables")]
public class Deployables : ScriptableObject
{
    public string ID = "000";
    public GameObject prefab;
    public int chanceToSpawnSecondaryPrefabs = 0;
    public GameObject[] secondaryPrefabs;
    public Sprite icon;
    public GameObject mapIcon;
    public string name;
    public bool isGreenTeam = false;
    public int price = 50;
}
