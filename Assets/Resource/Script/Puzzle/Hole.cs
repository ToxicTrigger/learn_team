using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public List<string> dropTags;

    private void OnTriggerEnter(Collider other) 
    {
        foreach( var tag in dropTags )
        {
            if( other.gameObject.tag.Equals(tag) )
            {
                Destroy(other.transform.root.gameObject); 
            }
        }
    }
}
