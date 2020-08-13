using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : State
{
    public string[] target_tag;
    public float radius = 10.0f;
    public bool target_found;
    public GameObject near_target;

    public Collider[] bound_targets;

    void look()
    {
        if (near_target != null)
        {
            var look = this.near_target.transform.position - this.transform.position;
            look.y = 0;
            this.transform.rotation = Quaternion.LookRotation(look);
        }
    }

    void ray()
    {
        if (this.near_target == null) return;
        Ray ray = new Ray(this.transform.position, this.near_target.transform.position - this.transform.position);
        RaycastHit hits;
        if (Physics.Raycast(ray, out hits, 1000  ))
        {
            if (hits.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
            {
                near_target = null;
            }
        }
    }
    
    void Update()
    {
        this.ray();
        
        if (target_tag.Length != 0)
        {
            var cols = Physics.OverlapSphere(this.transform.position, radius, LayerMask.GetMask( target_tag ));
            if (cols.Length == 0)
            {
                target_found = false;
                near_target = null;
                bound_targets = null;
                return;
            }

            bound_targets = cols;
            
            float min_distance = float.MaxValue;
            foreach (var VARIABLE in cols)
            {
                target_found = true;
                var distance = Vector3.Distance(this.transform.position, VARIABLE.gameObject.transform.position);
                if (min_distance > distance)
                {
                    min_distance = distance;
                    near_target = VARIABLE.gameObject;
                }
            }
            
            look();

        }
        else
        {
            target_found = false;
            near_target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere( this.transform.position , radius);
        if (this.near_target)
        {
            Gizmos.DrawLine( this.transform.position , this.near_target.transform.position );
        }
    }
}
