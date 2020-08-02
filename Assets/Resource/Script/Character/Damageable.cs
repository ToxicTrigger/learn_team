using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public float Hp = 1;
    public Text t_hp;
    public bool guard;
    public void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (t_hp)
        {
            t_hp.text = "hp : " + Hp;
        }
        if (Hp <= 0)
        {
            Die();
        }
    }
}
