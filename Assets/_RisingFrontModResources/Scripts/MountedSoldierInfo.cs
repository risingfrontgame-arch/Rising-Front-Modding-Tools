using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mounted Soldier", menuName = "Mounted Soldier")]
public class MountedSoldierInfo : ScriptableObject
{
    [Header("General Settings")]
    public float maxSpeed = 7f;
    public float health = 100f;
    public float range = 80f;
    public float fireRate = 1f;
    public float accuracy = .015f;
    public float reloadTime = 5f;
    public float damage = 35f;
    public int magSize = 5;
    public bool canDamageArmor = false;
    public float armorDamage = 10f;
    [Tooltip("Should only animate the soldier itself")]
    public RuntimeAnimatorController soldierAnimator;

    [Header("Weapon FX")]

    public ParticleSystem impactEffect;
    public ParticleSystem bloodEffect;
    public AudioClip[] weaponSounds;
    public float weaponSoundMaxRange = 200f;
    public GameObject weaponProjectile;
    public float projectileSpeed;

    [Header("Melee Settings")]
    public bool isMelee = false;
    public float attackDelay = 0f;
    [Header("If ranged, set to 1 more than the range of their weapon")]
    public float findTargetRange = 81f;

    [Header("Horse Settings")]
    public Material horseMaterial;
    public Material horseMaineMaterial;
    public GameObject[] horseBackGear;
    public GameObject[] horseHeadGear;
    [Header("Soldier Settings")]
    public SkinnedMeshRenderer soldierBody;
    public GameObject weapon;
    public GameObject[] headGear;
    public GameObject[] backGear;

}