using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class CustomUnitMaker : MonoBehaviour
{
    [Header("Soldier Type")]
    public SoldierStats mySoldier;


    [Header("Other Settings")]
    public Transform headGear;
   public Transform backGear;
   public Transform weapon;
   
    public SkinnedMeshRenderer body;
    public Animator anim;
    SoldierStats lastSoldierStats;

    // Start is called before the first frame update
    void Start()
    {
      
    }

  
    // Update is called once per frame
    void Update()
    {
        if(lastSoldierStats != mySoldier)
        {
            lastSoldierStats = mySoldier;
            RemoveOldSoldier();
            InitializeNewSoldier();
        }
    }

    void RemoveOldSoldier()
    {
        EquipmentInfo[] equipment = GetComponentsInChildren<EquipmentInfo>();
        foreach(EquipmentInfo equip in equipment)
        {
            if (equip.gameObject != body.gameObject)
            {
                DestroyImmediate(equip.gameObject);
            }
        }
    }
    void InitializeNewSoldier()
    {
        foreach (GameObject obj in mySoldier.headGear)
        {
            GameObject objects = Instantiate(obj, headGear);
        }
        foreach (GameObject obj in mySoldier.backGear)
        {
            GameObject objects = Instantiate(obj, backGear);
        }
        Instantiate(mySoldier.weaponModel, weapon);
        body.sharedMaterials = mySoldier.body.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
        body.sharedMesh = mySoldier.body.GetComponent<SkinnedMeshRenderer>().sharedMesh;
       // anim.SetInteger("WeaponType", mySoldier.weaponType);
        //anim.SetBool("Idle", true);
    }
}
