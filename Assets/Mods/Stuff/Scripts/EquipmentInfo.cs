using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInfo : MonoBehaviour
{
    [Header("0 = small items(ammo pouches, belts) 1 = medium size (rifles, gasmasks, equipment) 2 = large items (helmets, backpacks)")]
    [Range(0, 2)]
    public int lodSize = 1;
    //0 small
    //1 medium
    //2 large
}
