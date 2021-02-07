using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public ParticleSystem attackEffect;
    public string target_tag, target_layer;
    public float cool_down = 0.2f;
    public float Damage = 1.0f;

    public Element element;

    void Start()
    {
        
    }

    IEnumerator hit(float cool_down , Damageable hp)
    {
        if (hp)
        {
            if (!hp.guard || (hp.guard && Vector3.Dot(this.transform.forward.normalized, hp.transform.forward.normalized) < 0f) ) // 가드를 했는데 후면??
            {
                hp.Hp -= Damage;
            }
        }
        
        if( attackEffect ) this.attackEffect.Play();
        yield return new WaitForSeconds(cool_down);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(target_layer) && other.gameObject.tag.Equals(target_tag))
        {
            StartCoroutine(hit(cool_down, other.GetComponent<Damageable>() ));
            var tmp= GameObject.Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            Destroy( tmp.gameObject , 2.0f );
        }
    }
}
