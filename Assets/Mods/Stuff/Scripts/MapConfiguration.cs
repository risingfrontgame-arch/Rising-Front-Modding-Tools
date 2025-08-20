using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfiguration : MonoBehaviour
{

    public int gridWidth = 20;
    public int gridHeight = 20;
    public MapSO map;

    //void Start()
    //{
    //    GameObject obj = Instantiate(Resources.Load<GameObject>("Map Prefab"), transform.position, Quaternion.identity);
    //    CommandManager cmd = obj.GetComponentInChildren<CommandManager>();
    //    cmd.gridHeight = gridHeight;
    //    cmd.gridWidth = gridWidth;
    //    cmd.team1Spawn.position = GameObject.Find("Green HQ Position").transform.position;
    //    cmd.team2Spawn.position = GameObject.Find("Red HQ Position").transform.position;
    //    GetComponentInChildren<BoxCollider>().gameObject.SetActive(false);

    //}


}
