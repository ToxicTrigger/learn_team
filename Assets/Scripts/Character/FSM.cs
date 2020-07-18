using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public State current_state;
    public State previous_state;
    public State next_state;

    public List<State> my_states;

    public void init_my_states()
    {
        my_states = new List<State>();
        var get_states = this.GetComponents<State>();
        foreach (var VARIABLE in get_states)
        {
            my_states.Add(VARIABLE);
        }
    }
    
    void Start()
    {
        init_my_states();
    }

    void Update()
    {
        
    }
}
