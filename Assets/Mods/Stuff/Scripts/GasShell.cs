using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasShell : MonoBehaviour
{

    [SerializeField] float damage = 10f;
    [SerializeField] float gasLength = 8f;
    float gasRange = 5f;
    //public CommandManager cmd;
    float gasDamageRate = 1.9f;
    float nextGasDamage;
    [SerializeField] ParticleSystem gas;
    // public List<Pathfinding.SoldierAI>
    //Pathfinding.SoldierAI[] nearbySoldiers;
    void Start()
    {
        gas.Play();
        Invoke("DisableGas", gasLength);
        //cmd = FindObjectOfType<CommandManager>();
    }
    void DisableGas()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextGasDamage)
        {
            nextGasDamage = gasDamageRate + Time.time;
            // List<Pathfinding.SoldierAI> allNearby = new List<Pathfinding.SoldierAI>();
            //nearbySoldiers = cmd.GetNearbySoldiers(null, 2, transform.position, true);

            //foreach (Pathfinding.SoldierAI soldier in nearbySoldiers)
            {
                //if (soldier.hasGasMask == false)
                {
                    //if (Vector3.Distance(transform.position, soldier.transform.position) < gasRange)
                    {
                        //soldier.TakeDamage(damage, null);
                    }
                }
            }

            //nearbySoldiers = cmd.GetNearbySoldiers(null, 2, transform.position, false);
            
            //foreach (Pathfinding.SoldierAI soldier in nearbySoldiers)
            {
                //if (soldier.hasGasMask == false)
                {
                    //if (Vector3.Distance(transform.position, soldier.transform.position) < gasRange)
                    {
                        //soldier.TakeDamage(damage, null);
                    }
                }
            }
        }
    }
}