using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //если столкнулись с героем, то наносим ему урон
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }


    
    void Update()
    {
        
    }
}
