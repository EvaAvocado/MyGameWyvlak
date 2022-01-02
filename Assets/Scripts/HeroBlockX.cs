using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBlockX : MonoBehaviour
{
    private bool isHero = false;
    private float heroX;
    
    private bool isGrounded;
    private Collider2D ground; 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    void Start()
    {
        
    }

    void Update()
    {
        CheckGround();
        if (isHero && isGrounded)
        {
            Hero.Instance.transform.position = new Vector3(heroX, Hero.Instance.transform.position.y, Hero.Instance.transform.position.z);
        }
    }
    
    private void CheckGround()
    {
        Collider2D new_ground = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.025f, 0.5f), 0.0f, whatIsGround);
        isGrounded = new_ground;
        if (!isGrounded)
        {
            ground = null;      
        } else
        {
            if (ground != new_ground)
            {
                ground = new_ground;
            }
        }
    }

    void  OnTriggerEnter2D(Collider2D  other)
    {
        if (other.CompareTag("StopHeroX"))
        {
            isHero = true;
            heroX = Hero.Instance.transform.position.x;
        }
    }
    
    void  OnTriggerExit2D(Collider2D  other)
    {
        if (other.CompareTag("StopHeroX")) isHero = false;
    }
}
