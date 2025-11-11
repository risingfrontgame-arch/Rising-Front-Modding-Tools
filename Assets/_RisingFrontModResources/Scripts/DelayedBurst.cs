using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedBurst : MonoBehaviour
{
    [SerializeField] Transform[] shootPositions;
    [SerializeField] ParticleSystem[] muzzelFlashes;
    [SerializeField] float timeDelay = 10f;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = .1f;
    [SerializeField] AudioSource shotAudio;
    [SerializeField] float bulletVelocity = 2000f;
    [SerializeField] float aimCone = 3f;
    [SerializeField] float bulletDamage = 100f;

    [SerializeField] int bulletAmount = 100;
    [SerializeField] int startBulletAmount;
    [SerializeField] bool playOnAwake = false;
    float nextShot;

    float startTime;

    int shootPosIndex = 0;
    int muzzleflashIndex = 0;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Awake()
    {
        startBulletAmount = bulletAmount;
    }

    private void OnEnable()
    {
        if (playOnAwake)
        {
            timeDelay = 0;
            bulletAmount = startBulletAmount;
        }
    }

    private void Update()
    {
        if (Time.time - startTime > timeDelay)
        {
            if (Time.time - nextShot > fireRate)
            {
                nextShot = Time.time;
                if (bulletAmount > 0)
                {
                    Shoot();
                    bulletAmount--;
                }
            }
        }
    }

    void Shoot()
    {
        Transform shootPos = shootPositions[shootPosIndex];
        muzzelFlashes[muzzleflashIndex].Play();

        muzzleflashIndex++;
        shootPosIndex++;

        if (shootPosIndex >= shootPositions.Length)
            shootPosIndex = 0;
        if (muzzleflashIndex >= muzzelFlashes.Length)
            muzzleflashIndex = 0;

        GameObject obj = Instantiate(
            bullet,
            shootPos.position,
            Quaternion.Euler(
                new Vector3(
                    shootPos.eulerAngles.x + Random.Range(-aimCone, aimCone),
                    shootPos.eulerAngles.y + Random.Range(-aimCone, aimCone),
                    shootPos.eulerAngles.z
                )
            )
        );

        Bullet bull = obj.GetComponent<Bullet>();
        bull.damage = bulletDamage;
        shotAudio.Play();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (shootPositions == null || shootPositions.Length == 0)
            return;

        // --- Gizmo detail settings ---
        int coneSegments = 36;       // smoother rings
        int trajectorySteps = 200;   // longer arc sampling
        float timeStep = 0.08f;      // simulate ~16 seconds of flight (200 * 0.08)
        float maxGizmoRange = 5000f; // safety cutoff distance (5 km visual cap)
        float halfAngle = aimCone * 0.5f;
        Vector3 gravity = Physics.gravity;

        foreach (Transform shootPos in shootPositions)
        {
            if (shootPos == null)
                continue;

            Vector3 origin = shootPos.position;
            Vector3 forward = shootPos.forward;

            // 1️⃣ Draw central trajectory
            Gizmos.color = Color.red;
            Vector3 prevPos = origin;
            for (int i = 1; i <= trajectorySteps; i++)
            {
                float t = i * timeStep;
                Vector3 newPos = origin + forward * bulletVelocity * t + 0.5f * gravity * t * t;

                // Stop drawing if bullet exceeds visualization range
                if (Vector3.Distance(origin, newPos) > maxGizmoRange)
                    break;
                Gizmos.DrawLine(prevPos, newPos);
                prevPos = newPos;
            }

            Vector3 impactPos = prevPos;

            // 2️⃣ Draw curved cone following trajectory
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
            int ringInterval = 6;
            Vector3 prevCenter = origin;
            float maxDistance = Vector3.Distance(origin, impactPos);

            for (int i = 0; i <= trajectorySteps; i += ringInterval)
            {
                float t = i * timeStep;
                Vector3 center = origin + forward * bulletVelocity * t + 0.5f * gravity * t * t;
                Vector3 tangent;
                if (i < trajectorySteps - ringInterval)
                {
                    Vector3 next = origin + forward * bulletVelocity * (t + timeStep * ringInterval) + 0.5f * gravity * (t + timeStep * ringInterval) * (t + timeStep * ringInterval);
                    tangent = (next - center).normalized;
                }
                else tangent = (center - prevCenter).normalized;

                float distance = Vector3.Distance(origin, center);
                float radius = distance * Mathf.Tan(halfAngle * Mathf.Deg2Rad);
                Quaternion ringRot = Quaternion.LookRotation(tangent, Vector3.up);

                // Draw ring
                Vector3 ringPrev = Vector3.zero;
                for (int j = 0; j <= coneSegments; j++)
                {
                    float ang = (j / (float)coneSegments) * 360f;
                    Vector3 dir = ringRot * (Quaternion.AngleAxis(ang, Vector3.forward) * Vector3.right);
                    Vector3 point = center + dir * radius;
                    if (j > 0) Gizmos.DrawLine(ringPrev, point);
                    ringPrev = point;
                }

                // Connect ring to previous
                if (i > 0)
                {
                    Gizmos.color = new Color(1f, 0.4f, 0f, 0.08f);
                    for (int j = 0; j <= coneSegments; j += 4)
                    {
                        float ang = (j / (float)coneSegments) * 360f;
                        Vector3 dirA = ringRot * (Quaternion.AngleAxis(ang, Vector3.forward) * Vector3.right);
                        Vector3 pointA = center + dirA * radius;

                        Vector3 prevDirA = Quaternion.LookRotation((center - prevCenter).normalized, Vector3.up) *
                                           (Quaternion.AngleAxis(ang, Vector3.forward) * Vector3.right);
                        Vector3 pointPrev = prevCenter + prevDirA * (radius - 0.02f);
                        Gizmos.DrawLine(pointPrev, pointA);
                    }
                }

                prevCenter = center;
            }

            // 3️⃣ Impact ring
            float finalRadius = maxDistance * Mathf.Tan(halfAngle * Mathf.Deg2Rad);
            Quaternion impactRot = Quaternion.LookRotation((impactPos - origin).normalized, Vector3.up);
            Gizmos.color = new Color(1f, 0.8f, 0f, 0.6f);
            Vector3 lastPrev = Vector3.zero;
            for (int i = 0; i <= coneSegments; i++)
            {
                float angle = (i / (float)coneSegments) * 360f;
                Vector3 dir = impactRot * (Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right);
                Vector3 p = impactPos + dir * finalRadius;
                if (i > 0) Gizmos.DrawLine(lastPrev, p);
                lastPrev = p;
            }

            // 4️⃣ Muzzle marker
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(origin, 0.05f);
        }

        // Connect all shoot positions for visual reference
        Gizmos.color = new Color(0.3f, 0.8f, 1f, 0.4f);
        for (int i = 0; i < shootPositions.Length - 1; i++)
        {
            if (shootPositions[i] && shootPositions[i + 1])
                Gizmos.DrawLine(shootPositions[i].position, shootPositions[i + 1].position);
        }
    }
#endif
}
