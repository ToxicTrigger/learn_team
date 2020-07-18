using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public Vector3 movement;
    public float speed = 1.0f;
    private CharacterController _controller;
    private Summoner _summoner;
    
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
    }

    void updateRotate()
    {
        if( this.movement.x == 0 && this.movement.z == 0 ) return;
        this.transform.rotation = Quaternion.LookRotation(this.movement);
    }

    void move()
    {
        this._controller.Move(this.movement * speed);
    }

    void summon()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _summoner.summon();
        }
    }
    

    void Update()
    {
        this.setMovement();
        this.updateRotate();
        this.move();
        this.summon();
    }
}
