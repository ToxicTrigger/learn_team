using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    public Detect detect;
    public NavMeshAgent nma;
    public float attack_cool = 1.0f;
    public float attack_radius = 3.0f;
    public bool attacked = false;
    public Weapon weapon;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        detect = GetComponent<Detect>();
    }

    IEnumerator weapon_use()
    {
        attacked = true;
        if (weapon)
        {
            weapon.GetComponent<Collider>().enabled = true;
        }
        yield return new WaitForSeconds(attack_cool);
        attacked = false;
        if (weapon)
        {
            weapon.GetComponent<Collider>().enabled = false;
        }
    }

    void attack()
    {
        if (Vector3.Distance(detect.near_target.transform.position, transform.position) <= attack_radius && !attacked )
        {
            StartCoroutine(weapon_use());
        }
    }
    
    void Update()
    {
        if (detect == null || !detect.target_found ) return;
        nma.SetDestination(detect.near_target.transform.position);
        attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position , attack_radius);
    }
}
