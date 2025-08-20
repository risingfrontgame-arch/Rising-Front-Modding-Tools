using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier", menuName = "SoldierStats")]
public class SoldierStats : ScriptableObject
{
    //public string soldierType = "default type";
    [Range(0, 1)]
    public int unitType = 0;
    public RuntimeAnimatorController customAnimator;
    //0 rifleman
    //1 officer
    public bool canDamageArmor = false;
    public float armorDamage = 20f;
    public float health = 100f;
    [Range(.001f, .05f)]
    public float accuracy = .01f;
    [Range(0, 1)]
    public int weaponType = 0;
    public float shotCooldown = 1.5f;
    public bool isMeleeOnly = false;
    public float meleeCooldown = 1f;
    public float meleeDamages = 15f;
    public float meleeTargetRange = 20f;
    public int magSize = 5;
    public float reloadTime = 8f;
    public float damage = 25f;
    public float speedMultiplier = 1f;
    public float range = 60f;
    public GameObject bulletTracer;
    public float bulletTracerSpeed = 11f;
    //[ColorUsage(true, true)]
    // public Color tracerColor = new Color(191  , 42, 0 );
    public AudioClip shotSounds;
    public GameObject weaponModel;
    public GameObject weaponModel2;
    public GameObject body;
    public GameObject[] headGear;
    public GameObject[] backGear;

}