using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public Vector3 movement;
    public float speed = 1.0f;
    private Summoner _summoner;
    public Animator animator;
    public ParticleSystem swing;
    public Collider hitCollider;
    public float attack_cool = 0.15f;
    public GameObject Shield;
    public bool Shield_up;
    public MapBoard MapBoard;
    void Start()
    {
        _summoner = GetComponent<Summoner>();
    }

    void setMovement()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        this.movement.x = h;
        this.movement.z = v;
        if (this.animator)
        {
            animator.SetFloat( "Movement" , movement.normalized.magnitude );
        }
    }

    void updateRotate()
    {
        if( this.movement.x == 0 && this.movement.z == 0 ) return;
        this.transform.rotation = Quaternion.LookRotation(this.movement);
    }

    void move()
    {
        if ( ! this.animator.GetBool("Attack") )
        {
            if (movement.magnitude != 0)
            {
                var pos = this.transform.position;

                var next_pos = Vector3.MoveTowards(pos, pos + (movement * speed),
                    100.0f);
                this.transform.position = next_pos;
            }
        }
    }

    void summon()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _summoner.summon();
        }
    }

    IEnumerator attack_time()
    {
        animator.SetBool("Attack" , true );
        swing.Play();
        hitCollider.enabled = true;
        yield return new WaitForSeconds(attack_cool);
        animator.SetBool("Attack" , false );
        hitCollider.enabled = false;
    }
    
    void attack()
    {
        if (Input.GetKeyDown(KeyCode.S) && this.swing && this.animator && ! this.animator.GetBool("Attack") )
        {
            StartCoroutine(attack_time());
        }
    }

    void defence()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Shield.SetActive( true );
            Shield_up = true;
            GetComponent<Damageable>().guard = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            Shield.SetActive( false );
            Shield_up = false;
            GetComponent<Damageable>().guard = false;
        }
    }

    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Update()
    {
        this.setMovement();
        this.updateRotate();
        this.move();
        this.summon();
        this.attack();
        this.defence();
        Debug.Log( MapBoard.isUpPlane(this.transform.position));

    }
}
