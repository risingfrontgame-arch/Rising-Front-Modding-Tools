using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier", menuName = "SoldierStats")]
public class SoldierStats : ScriptableObject
{
    //public string soldierType = "default type";
    [Range(0, 1)]
    public int unitType = 0;
    [Tooltip("You can leave blank if you prefer default")]
    public RuntimeAnimatorController customAnimator;
    //0 rifleman
    //1 officer
    public float health = 100f;
    [Header("Weapon Stats")]
    [Range(.001f, .05f)]
    public float accuracy = .011f;
    [Range(0, 1)]
    [Tooltip("0 for Rifle, 1 for Pistols")]
    public int weaponType = 0;
    public float shotCooldown = 1.5f;
    public int magSize = 5;
    public float reloadTime = 8f;
    public float damage = 25f;
    public float speedMultiplier = 1f;
    public float range = 60f;
    public bool canDamageArmor = false;
    public float armorDamage = 20f;

    [Header("Melee")]
    public bool isMeleeOnly = false;
    public float meleeCooldown = 1f;
    public float meleeDamages = 15f;
    [Tooltip("Soldier will chase enemies within set range")]
    public float meleeTargetRange = 20f;

    [Header("Weapon VFX")]
    [Tooltip("You can leave blank if you prefer default")]
    public GameObject bulletTracer;
    [Tooltip("You can leave blank if you prefer default")]
    public ParticleSystem customDirtImpact;
    [Tooltip("You can leave blank if you prefer default")]
    public ParticleSystem customBloodImpact;
    public float bulletTracerSpeed = 11f;

    [Header("Grenade")]
    public GameObject grenadeObj;
    public float grenadeMaxRange = 30f;
    public float grenadeCoolDown = 200f;
    public int grenadeCount = 2;
    public float grenadeFuseTime = 4f;
    public float grenadeAccuracy = .8f; //1 = perfect accuracy, .8f = less accurate

    //[ColorUsage(true, true)]
    // public Color tracerColor = new Color(191  , 42, 0 );
    [Header("Weapon SFX")]
    public AudioClip shotSounds;
    [Header("Weapon")]
    public GameObject weaponModel;
    [Tooltip("Attaches to right wrist")]
    public GameObject weaponModel2;
    [Header("Body and Gear")]
    public GameObject body;
    public GameObject[] headGear;
    public GameObject[] backGear;

}