using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    [SerializeField] bool isExplosive = true;
    [SerializeField] float radius = 2f;
    [SerializeField] ParticleSystem explosion;
    AudioSource aud;
    [SerializeField] MeshRenderer mesh;
    ParticleSystem particle;
    [SerializeField] float vehicleDamage = 60f;
}