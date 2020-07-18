using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Element : MonoBehaviour
{
    public enum Type
    {
        Fire = 1, 
        Water = 0 ,
        Terra = -1 
    }

    public static float base_accel = 2.0f;

    public static float negative_accel = 0.5f;

    public static float get_counter(Element.Type a, Element.Type b)
    {
        if (a == b) return 1;
        else if (a == Type.Fire && b == Type.Fire) return 1; 
        else if (a == Type.Fire && b == Type.Terra) return base_accel;
        else if (a == Type.Fire && b == Type.Water) return negative_accel;
        else if (a == Type.Water && b == Type.Fire) return negative_accel; 
        else if (a == Type.Water && b == Type.Terra) return base_accel;
        else if (a == Type.Water && b == Type.Water) return 1;
        else if (a == Type.Terra && b == Type.Fire) return negative_accel;
        else if (a == Type.Terra && b == Type.Terra) return 1.0f;
        else if (a == Type.Terra && b == Type.Water) return base_accel;
        else return 0;
    }

    public static float get_counter(Element a, Element b)
    {
        return Element.get_counter(a.type, b.type);
    }

    // instance space=====================
    public Type type;
    


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (this.type == Type.Fire)
        {
            Gizmos.color = Color.red;
        }
        else if (this.type == Type.Terra)
        {
            Gizmos.color = Color.green;
        }
        else if (this.type == Type.Water)
        {
            Gizmos.color = Color.blue;
        }
        
        Gizmos.DrawCube( this.transform.position + Vector3.up * 5.4f, Vector3.one * 0.5f); 
    }

    // ===================================
}
