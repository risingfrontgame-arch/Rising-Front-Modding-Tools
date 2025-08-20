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
        if (playOnAwake == true)
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
        {
            shootPosIndex = 0;
        }
        if (muzzleflashIndex >= muzzelFlashes.Length)
        {
            muzzleflashIndex = 0;
        }
        GameObject obj = Instantiate(bullet, shootPos.transform.position, Quaternion.Euler(new Vector3(shootPos.transform.eulerAngles.x + Random.Range(-aimCone, aimCone), shootPos.transform.eulerAngles.y + Random.Range(-aimCone, aimCone), shootPos.transform.eulerAngles.z)));
        Bullet bull = obj.GetComponent<Bullet>();
        bull.damage = bulletDamage;
        shotAudio.Play();
    }

}