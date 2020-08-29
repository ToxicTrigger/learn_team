using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    public float Width = 10, Height = 10;

    public bool isUpPlane(Vector3 point)
    {
        var overlap = this.transform.position;
        overlap.x = overlap.x + Width;
        overlap.z = overlap.z + Height;
        var lp = this.transform.position;
        if ( overlap.z >= point.z && lp.z <= point.z && overlap.x >= point.x && lp.x <= point.x ) return true;
        return false;
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        var pos = this.transform.position;
        pos.x = pos.x + Width / 2;
        pos.z = pos.z + Height / 2;
        Gizmos.DrawWireCube(pos, new Vector3(Width, 0.1f,Height));
    }
}
