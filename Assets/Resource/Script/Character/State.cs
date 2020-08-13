using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public FSM fsm;

    private void Awake()
    {
        fsm = GetComponent<FSM>();
    }
}
