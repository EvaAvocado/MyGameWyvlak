using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    private float damage;

    private void Awake()
    {
        if (this.tag == "Plum")
        {
            damage = 10;
        }
            
    }
        //если столкнулись с героем, то наносим ему урон
        void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage(damage);
        }
    }


    
    void Update()
    {
        
    }
}
