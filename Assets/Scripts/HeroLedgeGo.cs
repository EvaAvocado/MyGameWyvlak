using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLedgeGo : MonoBehaviour
{
    private Hero hero;
    private BoxCollider2D box;
    [SerializeField] private LayerMask grounds;

    void Start()
    {
        hero = GetComponentInParent<Hero>();
        box = GetComponents<BoxCollider2D>()[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((grounds.value & 1 << collision.gameObject.layer) != 0)
        {
            box.enabled = false;
            // if(hero.getRb().velocity.y <= 0)
            // {
            //     hero.StartAnimLedgeGo();
            // }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((grounds.value & 1 << collision.gameObject.layer) != 0)
        {
            box.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((grounds.value & 1 << collision.gameObject.layer) != 0)
        {
            hero.StartAnimLedgeGo();
        }
    }


}
