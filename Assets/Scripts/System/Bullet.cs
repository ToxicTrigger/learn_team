using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public string target_tag;
    public string ignore_tag;
    public float Damage = 1.0f;
    public Vector3 target;
    public bool lockOn;
    public float speed = 3.0f;

    IEnumerator hit( Damageable target )
    {
        target.Hp -= Damage;
        yield return new WaitForSeconds( 2.0f );
    }

    void Die()
    {
        Destroy( this.gameObject );
        var effect = GameObject.Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy( effect.gameObject , 3.0f );
    }
    
    private void FixedUpdate()
    {
        if (Vector3.Distance(target, this.transform.position) <= float.Epsilon)
        {
            Die();
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed);
        this.transform.rotation = Quaternion.LookRotation( target - this.transform.position , Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals(ignore_tag)) return;
        Die();
        
        var damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            StartCoroutine(hit(damageable));
        }
    }
}
