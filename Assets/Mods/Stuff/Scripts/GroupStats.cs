using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Group", menuName = "GroupStats")]
public class GroupStats : ScriptableObject
{
    public string ID = "-1";
    public Material flagMat;
    public Material playerShirtColor;
    public bool isTeam1 = false;
    public Sprite groupIcon;
    public SoldierStats[] soldierTypes;
    public int platoonSize = 4;
    public int price = 100;
    public int timePeriod = 0;



    public void Initialize()
    { 

      
            Random.InitState((int)System.DateTime.Now.Ticks);
            Debug.Log("CHANGING ID NAME:" + this.name);
         //   ID = Random.Range(-999999999, 999999999);

        
    }
}
