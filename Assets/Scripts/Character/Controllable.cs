using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public Vector3 movement;
    public float speed = 1.0f;
    private CharacterController _controller;
    private Summoner _summoner;
    public Animator animator;
    public ParticleSystem swing;
    public Collider hitCollider;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
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
            this._controller.Move(this.movement * speed);
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
        yield return new WaitForSeconds(0.15f);
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

    void Update()
    {
        this.setMovement();
        this.updateRotate();
        this.move();
        this.summon();
        this.attack();
    }
}
