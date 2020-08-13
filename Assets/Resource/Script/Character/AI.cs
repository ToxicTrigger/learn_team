using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AI : MonoBehaviour
{
    public enum State
    {
        Offline,             // Disconnected AIManager.. 
        Idle,              // Idle State, Waiting for Interactive Other AI
        Find,             // Find Other AI
        Chasing,            // Move To him.. 
        Connected,         // Got him
        AttackReady,        // Ready for Attack
        Attack,             // Attack!
        Attacked,           // Hit!
        Damaged,             // Damaged.. 
        Die,                // Dead..
    }
    
    public AIManager ai_manager;
    public State state;
    public FSM fsm;
    public Damageable damageable;
    void Start()
    {
        ai_manager = GameObject.Find("AIManager").GetComponent<AIManager>();
        state = State.Idle;
        ai_manager.pushAI( this );
        damageable = GetComponent<Damageable>();
    }

    private void OnDestroy()
    {
        state = State.Offline;
    }

    public void Die()
    {
        DestroyImmediate(this.gameObject);
    }

    void Update()
    {
        if (damageable.Hp <= 0)
        {
            Die();
        }
    }
    
    private void OnDrawGizmos()
    {
        switch (state)
        {
            case State.Offline: 
                Gizmos.color = Color.grey;
                break;
            case State.Idle:
                Gizmos.color = Color.green;
                break;
        }
        
        Gizmos.DrawSphere(this.transform.position + Vector3.up * 6, 0.5f);
    }
}
