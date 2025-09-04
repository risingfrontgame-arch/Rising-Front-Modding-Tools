using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using EZCameraShake;

public class StaticEmplacement : MonoBehaviour
{
    Camera cam;
   

    // ----- Settings -----
    [Header("General Settings")]
    public Animator animator;
    public Transform targetDetectionTransform;
    public GameObject cameraHolder;
    [SerializeField] float fireRate = .1f;
    public float vertAngleMax = 85f;
    public float vertAngleMin = -55f;
    public float attackAngleLimit = 60f;
    public bool limitRotation = false;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;
    [SerializeField] AudioSource aud;
    public Transform[] standPositions;
 // [SerializeField] AudioSource aud;
    [Header("Cannon Variables")]
    public bool useHighAngle = true; // Set to true for high angle, false for low angle
    public bool isCannon;
    public Transform cannonBase;
    public Transform cannonBarrel;
    public Transform barrelObj;
    public bool requireNearbySoldier = true;
    public float maxRange = 500f;
    public int burstCount = 5;
    public float burstCooldown = 0;
    int bulletsInBurst;
    float nextBurst;
    public bool makePlayerUseBurst = false;

    [Header("Machinegun Variables")]

    [SerializeField] Transform turret;
    // ----- References -----
  
   
  
 


    // ----- Serialized Fields -----
    [Header("DO NOT TOUCH THESE VARIABLES")]
    [SerializeField] GameObject playerDetectionObj;



   // Pathfinding.SoldierAI[] nearbySoldiers;

    // [SerializeField] Transform smoothObj;
    //[SerializeField] GameObject camHold;


    // ----- Private Fields -----

  //  [HideInInspector]
 //   public GameReference game;
   // [HideInInspector]
  //  public GridButton[] grids;
  //  [HideInInspector]
  //  public Pathfinding.SoldierAI soldier;
    [Header("Private Fields")]
 //   private CommandManager cmdManager;
    private Vector2 mouseLook;
    private Vector2 smoothV;
   // private HitMarkerSound hitSounds;
    private float nextShot;
    private float rotY;
    private float rotX;
    private float lastYpos;
    [HideInInspector]
    public float lastHealth;
  //  private Health playerHealth;
   // private PlayerUI uiPlayer;
  //  private CameraShaker camShake;
    private GameObject myPlayer;
   
    
    public int invertControls = -1;
    [HideInInspector]
    public bool greenEmplacement = true;
    // ----- Shooting and Recoil -----
    [Header("Shooting and Recoil")]
    public float aimCone = .6f;
  //  float bulletMass = .25f;
   // float bulletForce = 400f;
    public float recoilSpeed = 100f;
    public float recoilRecoverySpeed = 15f;
    public float barrelRecoil = .5f;
    Vector3 originalBarelPos;
    Vector3 targetPos;
    bool startedRecoil;
   
    float sensitivity = .6f;
    float smoothing = 2.0f;

   // [HideInInspector]
   // public Pathfinding.SoldierAI distantTarget;
    [HideInInspector]
    public bool isAimedAtTarget = false;
    [HideInInspector]
    public float aimThreshold = 0.1f;
  //  [HideInInspector]
    public float initialVelocity = 48f;
    float angleTolerance = 1.0f;  // Set your desired angle tolerance in degrees
    [HideInInspector]
    public int trajectoryResolution = 100;
    [HideInInspector]
    public float timeResolution = 0.1f;

    void OnDrawGizmos()
    {
        if (cannonBarrel != null)
        {
            Vector3 initialPosition = cannonBarrel.transform.position;
            Vector3 initialDirection = cannonBarrel.transform.TransformDirection(Vector3.forward) * initialVelocity;

            Gizmos.color = Color.red;
            Vector3 previousPosition = initialPosition;

            for (int i = 1; i <= trajectoryResolution; i++)
            {
                float time = timeResolution * i;
                Vector3 newPosition = initialPosition + initialDirection * time + 0.5f * Physics.gravity * time * time;
                Gizmos.DrawLine(previousPosition, newPosition);
                previousPosition = newPosition;
            }
        }
    }



}

