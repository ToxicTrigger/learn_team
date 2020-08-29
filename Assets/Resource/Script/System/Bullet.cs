using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public string target_tag;
    public List<string> ignore_tags;
    public float Damage = 1.0f;
    public Vector3 target;
    public bool lockOn;
    public float speed = 3.0f;
    public bool live = true;

    IEnumerator hit( Damageable target )
    {
        target.Hp -= Damage;
        yield return new WaitForSeconds( 2.0f );
    }

    void Die()
    {
        var particles = GetComponentsInChildren<ParticleSystem>();
        foreach (var VARIABLE in particles)
        {
            VARIABLE.Stop();
            if (VARIABLE.name.Equals("Range_Bullet") || VARIABLE.name.Equals("Range_light"))
            {
                VARIABLE.GetComponent<Renderer>().enabled = false;
            }
        }
        Destroy( this.gameObject ,1.0f );
        var effect = GameObject.Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy( effect.gameObject , 3.0f );
        live = false;
    }
    
    private void FixedUpdate()
    {
        if (Vector3.Distance(target, this.transform.position) <= float.Epsilon && live)
        {
            Die();
            return;
        }

        if (live)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed);
            this.transform.rotation = Quaternion.LookRotation( target - this.transform.position , Vector3.up);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach( var s in ignore_tags )
        {
            if( s.Equals( other.gameObject.tag ) ) return;
        } 
        
        Debug.Log( other.gameObject.name );
        Die();
        
        var damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            StartCoroutine(hit(damageable));
        }
    }
}
