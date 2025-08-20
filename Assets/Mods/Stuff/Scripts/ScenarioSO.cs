using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario Data")]
public class ScenarioSO : ScriptableObject
{

    public string mapName;
    public string scenarioName;
    public string creatorName = "myName";
    public string hiddenMapName;
    public string description;
    public bool isAttacking = false;
    public float battleLength = 800f;

    public int mapIndex = 0;
    public float timeOfDay = .6f;
    public bool dayNightCycle = false;
    public float timeOffest = 65f;
    public int weatherType = 1;

    public bool canCommandEnemyTeam = true;
    public bool isRedTeam = true;


    public int redStartingMoney = 5000;
    public int greenStartingMoney = 5000;
    public int greenIncome = 2;
    public int redIncome = 2;

    public bool includeAllUnits = true;
    public bool includeAllBuildables = true;

    public List<int> includedUnits = new List<int>();
    public List<int> includedBuildables = new List<int>();
    public Sprite scenarioIcon;
  //  public OrderQueueSO scriptedBattle;

    public Vector3 SpectateCamPosition;
    public Vector3 RedHQPosition;
    public Vector3 GreenHQPosition;

    public int GridXSize = 30;
    public int GridZSize = 30;
    public List<int> ID = new List<int>();

    public List<string> buildableIndex = new List<string>();
    public List<Vector3> buildableLocation = new List<Vector3>();
    public List<Vector3> buildableRotation = new List<Vector3>();
    public List<float> buildableTime = new List<float>();
    public List<bool> buildableTeam = new List<bool>();



    public List<int> orderType = new List<int>();
    public List<bool> orderTeam = new List<bool>();
    public List<int> platoonIndex = new List<int>();
    public List<float> orderDelay = new List<float>();
    public List<string> buyIndex = new List<string>();

    public List<int> MovePositionX = new List<int>();
    public List<int> MovePositionZ = new List<int>();

    public List<Vector3> deployPosition = new List<Vector3>();

    public bool isCustomScenario = false;
    public int timePeriod = 0;

}