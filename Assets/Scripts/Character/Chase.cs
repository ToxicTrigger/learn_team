using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    public Detect detect;
    public NavMeshAgent nma;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        detect = GetComponent<Detect>();
    }
    
    void Update()
    {
        if (detect == null || !detect.target_found ) return;
        nma.SetDestination(detect.near_target.transform.position);
    }
}
