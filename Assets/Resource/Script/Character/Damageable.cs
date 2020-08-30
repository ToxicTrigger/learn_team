using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public float Hp = 1;
    float MaxHp;
    public bool guard;

    private void Awake() 
    {
        MaxHp = Hp;
    }

    public float getMaxHp()
    {
        return MaxHp;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            Die();
        }
    }
}
