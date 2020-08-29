using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private Camera cam;
    public bool negtiveZ;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.LookAt(cam.transform , Vector3.up * (negtiveZ ? -1 : 1));
    }
}
