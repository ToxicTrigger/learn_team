using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public Damageable hp;
    public Image image;
    public SpriteRenderer renderer;

    Material hp_shader;
    void Start()
    {
        hp = hp ? hp : GetComponentInParent<Damageable>();
        hp_shader = renderer ? renderer.material : (image ? image.material : null);
    }

    void Update()
    {
        if( !hp ) return;
        if( hp_shader ) 
        {
            hp_shader.SetFloat("_Power" , hp.Hp / hp.getMaxHp());
        }
    }
}
