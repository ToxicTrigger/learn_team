using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : State
{
    public Transform target;
    public Bullet bullet;
    public float cool_down = 1.0f;
    public Detect Detect;
    public bool shooting;
    void Start()
    {
        this.Detect = GetComponent<Detect>();
    }
    
    private void FixedUpdate()
    {
        if (Detect.near_target == null) return;
        this.target = Detect.near_target.transform;
        if (!shooting)
        {
            StartCoroutine(shot(cool_down));
        }
    }

    public IEnumerator shot(float cool_down)
    {
        if (this.target == null) yield break;
        this.shooting = true;
        var b = GameObject.Instantiate(bullet, transform.position, Quaternion.LookRotation( target.position - this.transform.position , Vector3.up));
        b.target = target.position;
        yield return new WaitForSeconds( cool_down );
        this.shooting = false;
    }
}
