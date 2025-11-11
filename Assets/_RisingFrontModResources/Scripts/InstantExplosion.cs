using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantExplosion : MonoBehaviour
{
   
    [SerializeField] float radius = 4f;
    [SerializeField] ParticleSystem explosion;
    AudioSource aud;
    public float deployableDamage = 100f;
    ParticleSystem particle;
    public float destroyTime = 7f;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    aud = GetComponent<AudioSource>();

    //    Explode();
    //}



    //void Explode()
    //{
    //    aud.Play();
    //    particle = Instantiate(explosion, transform.position, explosion.transform.rotation);
    //    particle.Play();
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
    //    Invoke("DestroyObj", 8f);


    //    foreach (var hitCollider in hitColliders)
    //    {

    //        DeployableObject obj = hitCollider.GetComponentInParent<DeployableObject>();
    //        if (obj != null)
    //        {
    //            obj.TakeDamage(deployableDamage);
    //        }
    //        Pathfinding.SoldierAI ais = hitCollider.GetComponentInParent<Pathfinding.SoldierAI>();
    //        if (ais != null)
    //        {
    //            ais.TakeDamage(150f, transform.gameObject);
    //        }
    //      if(hitCollider.transform.name == "Player Detection")
    //        {
    //            hitCollider.GetComponentInParent<Health>().TakeDamages(50f);
    //        }
    //    }
    //}

    //void DestroyObj()
    //{
    //    Destroy(particle.gameObject);
    //    Destroy(gameObject);
    //}
}
