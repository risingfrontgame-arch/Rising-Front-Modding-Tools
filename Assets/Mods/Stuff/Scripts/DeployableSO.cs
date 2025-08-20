using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Deployable Object")]
public class DeployableSO : ScriptableObject
{
    public int ID ;
    public Sprite icon;
    public int price;
    public string objName;
    public GameObject objPrefab;
    public int timePeriod = 0;

    private void OnEnable()
    {
        objPrefab.GetComponent<DeployableObject>().buildableID = ID;
    }
    public void Initialize()
    {

        Random.InitState((int)System.DateTime.Now.Ticks);
        Debug.Log("CHANGING ID NAME:" + this.name);
        ID = Random.Range(-999999999, 999999999);


    }
}


