using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour {

  //  public OptionsSO optionSave;
    #region Weapon Info
    [Header("0 = gun, 1 = throwableItem, 2 = melee, 3 = bandage")]
    [SerializeField] int itemType = 0;
    // 0 = gun
    // 1 = throwable item
    // 2 = melee
    // 3 = bandage
    [Header(" 0 = rifle, 1 = pistol, 2 = shotgun, 3 = grenades, 4 = bandages")]
    public int ammoType = 0;
    bool pinPulled = false;
    [Header("throwable Info")]
    [SerializeField] float throwCooldown = 2f;
    [SerializeField] float bulletVelocity = 2000f;
    float HoldTime;
    float minHoldTime = .9f;
    [SerializeField] GameObject throwableItem;
    // CameraShaker camShake;

 
    //0 = rifle ammo
    //1 = pistol ammo
    //2 = shotgun ammo
    //3 = grenades
    //4 = bandages
    [Header("Weapon Info")]
    [SerializeField] ParticleSystem shellEject;
    [SerializeField] float weaponDamage = 100f;
    [HideInInspector]
    [SerializeField] int StartingAmmo = 30;
    [SerializeField] float fireRate = .2f;
    [SerializeField] int magazineSize = 5;
    [SerializeField] float range = 200f;
    [SerializeField] bool fullAuto = false;
    [SerializeField] GameObject bulletObj;
    [SerializeField] float aimCone = 1f;
    [SerializeField] int projectileCount = 1;
    #endregion

    #region misc
    [Header("Misc")]
    [SerializeField] AudioClip aimSound;
    [SerializeField] AudioClip shotSound;
    [SerializeField] AudioClip drawSound;
    [SerializeField] AudioClip holsterSound;
    [HideInInspector]
    [SerializeField] GameObject firePosition;
    [SerializeField] float aimBobMultiplier = .3f;
    [SerializeField] float aimRunMultiplier = .5f;
    [SerializeField] float aimFov = 1f;
    [HideInInspector]
    [SerializeField] ParticleSystem impact;
    [SerializeField] ParticleSystem muzzelFlash;
    #endregion

    #region Recoil
    [Header("Recoil")]
    [SerializeField] float centerSpeeds = 4f;
    [SerializeField] float vertRecoils = 4f;
    [SerializeField] float horRecoils= -1f;
    [SerializeField] float weaponKicks = -.2f;
    [SerializeField] float weaponCenters = .01f;
    #endregion


    #region Bolt Action
    [Header("Bolt Action")]
    [SerializeField] bool boltAction = false;
    [SerializeField] float boltTime = 1.2f;
    [SerializeField] AudioClip boltSound;
    [SerializeField] float boltSoundTime = .2f;
    [HideInInspector]
    [SerializeField] bool ChamberCoolDown = false;
    [SerializeField] float ejectTime = .2f;
    #endregion


    #region Reload
    [SerializeField] float reloadTime;
    [SerializeField] float ReloadSound1Time;
    [SerializeField] float ReloadSound2Time;
    [SerializeField] float ReloadSound3Time;
    [SerializeField] bool singleReload = false;
    [SerializeField] float singleReloadTime = 1f;
    [SerializeField] AudioClip ReloadSound1;
    [SerializeField] AudioClip ReloadSound2;
    [SerializeField] AudioClip ReloadSound3;
    #endregion
  
    [HideInInspector]
    public bool lowerWeapon = false;
    public Animator anim;

    #region Private Variables
    bool roundInChamber = true;
    [HideInInspector]
    public int bulletsInMag;
    float startFOV;
 //   Loadout loadoutHolder;
    AudioSource aud;
    public AudioSource shotAud;
  // Controller player;
    Camera cam;
    //  Recoil kick;
    [HideInInspector]
    public bool isAiming;
    float targetFOV;
    bool isReloading;
   // public Inventory inv;
  //  HitMarkerSound hitSound;
    SkinnedMeshRenderer arms;

    #endregion

    #region Bandage Variables

    bool isBandaging = false;
    float bandageTime = 3f;
    float healAmount = 50f;
    #endregion

    [HideInInspector]
    public bool isEnabled  = true;

   // GameReference game;
  
}
