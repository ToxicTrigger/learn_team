using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    public enum Reason
    {
        Okay, 
        Overlaped,
        NoManna,
    }
    
    public GameObject summablePrefab;

    public Reason summon()
    {
        GameObject.Instantiate(summablePrefab , transform.position , transform.rotation , null);
        return Reason.Okay;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
