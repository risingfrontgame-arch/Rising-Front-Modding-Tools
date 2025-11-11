using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Player Item", menuName = "Player Item")]
public class PlayerItem : ScriptableObject
{
    public GameObject itemPrefab;
    public float itemWeight = 5;
    public string itemName = "new item";
    public Sprite itemIcon; 

}
