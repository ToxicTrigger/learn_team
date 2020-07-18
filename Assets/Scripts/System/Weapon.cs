using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public string target_tag, target_layer;
    public float cool_down = 0.2f;
    public float Damage = 1.0f;
    void Start()
    {
        
    }


    // Update is called once per frame

    void Update()
    {
        
    }

    IEnumerator hit(float cool_down , AI ai )
    {
        if (ai.fsm)
        {
            ai.fsm.updateState = false;
            ai.damageable.Hp -= Damage;
        }
        
        yield return new WaitForSeconds(cool_down);

        if (ai.fsm)
        {
            ai.fsm.updateState = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(target_layer) && other.gameObject.tag.Equals(target_tag))
        {
            StartCoroutine(hit(cool_down, other.GetComponent<AI>()));
            var tmp= GameObject.Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            Destroy( tmp.gameObject , 2.0f );
        }
    }
}
