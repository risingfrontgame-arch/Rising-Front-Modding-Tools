using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem blood;
    [SerializeField] ParticleSystem dirt;
    [SerializeField] GameObject instantExplosion;
    [SerializeField] bool updateBulletRotation = false;
    public bool isExplosive = false;
    [SerializeField] Rigidbody rb;
 [SerializeField]   TrailRenderer mesh;
// public   HitMarkerSound hitSound;
    public float bulletVelocity = 2000f;
    public float damage = 100f;
    public float bulletDestroyTime = 17f;
    // public GameReference game;

}
