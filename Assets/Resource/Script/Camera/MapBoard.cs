using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    public enum Direction 
    {
        Center,
        Up,
        Down,
        Left,
        Right,

		LeftUp,  // \
		RightUp, // /
		LeftDown, // / 
		RightDown, // \
        Unknown
    }

    public enum Axis
    {
        X,
        Z
    }

    public float Width = 10, Height = 10;

    public Vector3 getCenter()
    {
        var overlap = this.transform.position;
        overlap.x = overlap.x + Width;
        overlap.z = overlap.z + Height;
        return overlap;
    }

    public bool isUpPlane(Vector3 point)
    {
        var overlap = this.getCenter();
        var lp = this.transform.position;
        if ( overlap.z >= point.z && lp.z <= point.z && overlap.x >= point.x && lp.x <= point.x ) return true;
        return false;
    }

    public bool isBetweenAxis( Axis axis, Vector3 point )
    {
        var center = this.getCenter();
        if( axis == Axis.X && point.x >= this.transform.position.x && point.x <= this.transform.position.x + Width ) return true;
        if( axis == Axis.Z && point.z >= this.transform.position.z && point.z <= this.transform.position.z + Height ) return true;

        return false;
    }

    public Direction GetDirection( Vector3 point )
    {
		if( isUpPlane( point ) ) return Direction.Center;
        var center = this.getCenter();

        if( point.x < center.x && this.isBetweenAxis(Axis.Z , point) ) return Direction.Left;
        else if( point.x > center.x && this.isBetweenAxis(Axis.Z , point) ) return Direction.Right;
        else if( point.z > center.z && this.isBetweenAxis(Axis.X , point) ) return Direction.Up;
        else if( point.z < center.z && this.isBetweenAxis(Axis.X , point) ) return Direction.Down;
        else if( point.x < center.x && point.z > this.transform.position.z + Height ) return Direction.LeftUp;
        else if( point.x < center.x && point.z < this.transform.position.z ) return Direction.LeftDown;
        else if( point.x > center.x && point.z > this.transform.position.z + Height ) return Direction.RightUp;
        else if( point.x > center.x && point.z < this.transform.position.z ) return Direction.RightDown;
        return Direction.Unknown;
    }

    private void OnDrawGizmos()
    {
        var pos = this.transform.position;
        pos.x = pos.x + Width / 2;
        pos.z = pos.z + Height / 2;
        Gizmos.DrawWireCube(pos, new Vector3(Width, 0.1f,Height));
    }
}
