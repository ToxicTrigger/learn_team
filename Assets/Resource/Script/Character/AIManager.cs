using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public enum State
    {    
        Alive,
        Dead, 
    }
    
    public Dictionary<int, AI> ais;
    
    private Dictionary<int, AI> GetAllSceneAIs()
    {
        Dictionary<int, AI> ais = new Dictionary<int, AI>();
        var tmp_ais = GameObject.FindObjectsOfType<AI>();

        foreach (var ai in tmp_ais)
        {
            ai.ai_manager = this;
            ai.state = AI.State.Idle;
            ais.Add(ai.GetHashCode() , ai);
        }
        
        return ais;
    }

    public bool pushAI(AI ai)
    {
        if (ais == null)
        {
            ais = new Dictionary<int, AI>();
        }
        
        if (ais.ContainsKey(ai.GetHashCode())) return false;
        ais.Add(ai.GetHashCode() , ai);
        return true;
    }


    public void clearAi()
    {
        foreach (var VARIABLE in ais)
        {
            if (VARIABLE.Value.state == AI.State.Offline)
            {
                ais.Remove(VARIABLE.Key);
            }
        }
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
