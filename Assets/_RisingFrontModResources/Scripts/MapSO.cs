using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "MapData", menuName = "MapDataSO")]
public class MapSO : ScriptableObject
{

    public Sprite mapIcon;
    public string sceneName = "Sample Scene";
    public string mapName = "new map";
    public string mapDescription = "add map description here...";
    [HideInInspector]
    public ScenarioSO defaultScenario;
    [HideInInspector]
    public int index;
}
