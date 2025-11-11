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
    public float cannonRotateSpeed = .5f;
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
        // ---- Trajectory Gizmo ----
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

        // ---- Curved Aim Cone Gizmo (follows trajectory) ----
        if (cannonBarrel != null)
        {
            Vector3 origin = cannonBarrel.position;
            Vector3 initialVelocityVec = cannonBarrel.TransformDirection(Vector3.forward) * initialVelocity;
            float halfAngle = aimCone * 0.5f;
            int coneSegments = 24;          // number of radial lines per ring
            int ringCount = trajectoryResolution / 4; // number of rings along the curve (density)
            float step = timeResolution * 4; // spacing between rings

            List<Vector3> trajectoryPoints = new List<Vector3>();
            trajectoryPoints.Add(origin);

            // --- Sample trajectory points ---
            for (int i = 1; i <= ringCount; i++)
            {
                float t = step * i;
                Vector3 newPos = origin + initialVelocityVec * t + 0.5f * Physics.gravity * t * t;
                trajectoryPoints.Add(newPos);
            }

            // --- Draw the curved cone along the trajectory ---
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.6f);

            for (int i = 0; i < trajectoryPoints.Count; i++)
            {
                Vector3 pos = trajectoryPoints[i];
                Vector3 forwardDir;
                if (i < trajectoryPoints.Count - 1)
                    forwardDir = (trajectoryPoints[i + 1] - pos).normalized;
                else
                    forwardDir = (pos - trajectoryPoints[i - 1]).normalized;

                // Radius increases linearly with distance along the arc
                float distance = Vector3.Distance(origin, pos);
                float radius = distance * Mathf.Tan(halfAngle * Mathf.Deg2Rad);

                Quaternion rot = Quaternion.LookRotation(forwardDir, Vector3.up);
                Vector3 prevPoint = Vector3.zero;
                List<Vector3> ringPoints = new List<Vector3>();

                for (int j = 0; j <= coneSegments; j++)
                {
                    float angle = (j / (float)coneSegments) * 360f;
                    Vector3 dir = rot * (Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right);
                    Vector3 point = pos + dir * radius;
                    ringPoints.Add(point);

                    if (j > 0)
                        Gizmos.DrawLine(prevPoint, point);
                    prevPoint = point;
                }

                // Connect to previous ring to form the surface
                if (i > 0)
                {
                    List<Vector3> prevRing = new List<Vector3>();
                    Vector3 prevPos = trajectoryPoints[i - 1];
                    float prevDistance = Vector3.Distance(origin, prevPos);
                    float prevRadius = prevDistance * Mathf.Tan(halfAngle * Mathf.Deg2Rad);
                    Quaternion prevRot = Quaternion.LookRotation(
                        (pos - prevPos).normalized, Vector3.up
                    );

                    for (int j = 0; j <= coneSegments; j++)
                    {
                        float angle = (j / (float)coneSegments) * 360f;
                        Vector3 dirPrev = prevRot * (Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right);
                        Vector3 pointPrev = prevPos + dirPrev * prevRadius;
                        prevRing.Add(pointPrev);

                        // Connect ring segments
                        Gizmos.color = new Color(1f, 0.4f, 0f, 0.15f);
                        Gizmos.DrawLine(pointPrev, ringPoints[j]);
                    }
                }
            }

            // Optional: draw the central trajectory line for clarity
            Gizmos.color = Color.red;
            for (int i = 0; i < trajectoryPoints.Count - 1; i++)
                Gizmos.DrawLine(trajectoryPoints[i], trajectoryPoints[i + 1]);
        }
    }





}

