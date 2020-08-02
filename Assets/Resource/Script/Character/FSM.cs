using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public State current_state;
    public State previous_state;

    public List<State> my_states;
    public bool updateState = true;
    public void init_my_states()
    {
        my_states = new List<State>();
        var get_states = this.GetComponents<State>();
        foreach (var VARIABLE in get_states)
        {
            VARIABLE.enabled = false;
            my_states.Add(VARIABLE);
        }
    }
    
    void Start()
    {
        init_my_states();
        my_states[0].enabled = true;
        current_state = my_states[0];
    }

    public void nextState(State state)
    {
        if (!state) return;
        this.previous_state = this.current_state;
        this.previous_state.enabled = false;
        state.enabled = true;
        this.current_state = state;
    }
    
    void Update()
    {
        if (!updateState)
        {
            foreach (var VARIABLE in my_states)
            {
                VARIABLE.enabled = false;
            }
        }
        else
        {
            foreach (var VARIABLE in my_states)
            {
                VARIABLE.enabled = true;
            }
        }
    }
}
