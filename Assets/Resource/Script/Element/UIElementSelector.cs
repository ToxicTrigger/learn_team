using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementSelector : MonoBehaviour
{
    public Element element;
    public Sprite fire;
    public Sprite terra;
    public Sprite water;

    public Image image;
    public SpriteRenderer renderer;
    
    void Start()
    {
        element = element ? element : this.GetComponentInParent<Element>();
    }

    void Update()
    {
        if( !element ) return ;
        switch( element.type )
        {
            case Element.Type.Fire : 
                if( image ) image.sprite = fire;
                if( renderer ) renderer.sprite = fire;
            break;

            case Element.Type.Terra : 
                if( image ) image.sprite = terra;
                if( renderer ) renderer.sprite = terra;
            break;

            case Element.Type.Water : 
                if( image ) image.sprite = water;
                if( renderer ) renderer.sprite = water;
            break;
        }
    }
}
