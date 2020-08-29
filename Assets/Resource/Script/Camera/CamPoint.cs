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
        if (MapBoard.isUpPlane(point.position))
        {
            this.transform.position = Vector3.Lerp(this.transform.position, point.position, Time.deltaTime * moveSpeed);
        }
    }
}
