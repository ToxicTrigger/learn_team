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

    public bool move_done;
    public bool move1, move2;
    public Vector3 des1 , des2;

    public float des_p_1 = 4;
    public float des_p_2 = 2;

    IEnumerator hit( Damageable target )
    {
        target.Hp -= Damage;
        yield return new WaitForSeconds( 2.0f );
    }

    private void Start() 
    {
        des1 = this.transform.position;
        des2 = target;

        des1.y = des1.y + 10;

        des2.y = target.y + 8;
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

        if (live && move_done)
        {
            this.transform.position = Vector3.MoveTowards( this.transform.position, target, speed );
            this.transform.rotation = Quaternion.LookRotation( target - this.transform.position , Vector3.up );
        }
        else if( !move_done )
        {
            if( !move1 && Vector3.Distance( this.transform.position , this.des1 ) > float.Epsilon + 2 ) 
            {
                this.transform.position = Vector3.Lerp(this.transform.position , des1 , speed * 0.3f ) ;
                this.transform.rotation = Quaternion.Lerp( this.transform.rotation, Quaternion.LookRotation( des1 - this.transform.position , Vector3.up ) , speed );
            }else if(Vector3.Distance( this.transform.position , this.des1 ) <= float.Epsilon  + 2 ) {
                move1 = true;
            }

            if( !move2 && move1 && Vector3.Distance( this.transform.position , this.des2 ) > float.Epsilon  + 2) 
            {
                this.transform.position = Vector3.Lerp(this.transform.position , des2 , speed * 1.3f ) ;
                this.transform.rotation = Quaternion.Lerp( this.transform.rotation, Quaternion.LookRotation( des2 - this.transform.position , Vector3.up ) , speed );
            }else if(move1 && Vector3.Distance( this.transform.position , this.des2 ) <= float.Epsilon + 2 ){
                move2 = true;
            }
        }

        if( move1 && move2 ) move_done = true;
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
