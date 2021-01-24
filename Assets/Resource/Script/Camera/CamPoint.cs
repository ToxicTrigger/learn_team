using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class CamPoint : MonoBehaviour
{
    public MapBoard MapBoard;
    public Transform point;
    public float moveSpeed = 1.0f;
    void Update()
    {
        var direction = MapBoard.GetDirection( point.position );

        if (direction == MapBoard.Direction.Center)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, point.position, Time.deltaTime * moveSpeed);
        }
        else if( direction == MapBoard.Direction.Left || direction == MapBoard.Direction.Right  )
        {
            var stuck = Vector3.Lerp(this.transform.position, point.position, Time.deltaTime * moveSpeed);
            stuck.x = transform.position.x;
            transform.position = stuck;
        }
        else if( direction == MapBoard.Direction.Up || direction == MapBoard.Direction.Down )
        {
            var stuck = Vector3.Lerp(this.transform.position, point.position, Time.deltaTime * moveSpeed);
            stuck.z = transform.position.z;
            transform.position = stuck;
        }
    }
}
