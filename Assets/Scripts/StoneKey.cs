using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneKey : MonoBehaviour
{
    private Animator anim;
    private int lives;
    private GameObject obj;
    private bool isHero = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        lives = Hero.Instance.lives;
        obj = this.gameObject;
        
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.transform.GetChild(3).gameObject.SetActive(false);
        obj.transform.GetChild(4).gameObject.SetActive(false);
    }

    void Update()
    {
        if (lives == 3)
        {
            obj.transform.GetChild(0).GetComponent<Animator>().Play("shine");
        }

        if (lives != 3)
        {
            obj.transform.GetChild(0).GetComponent<Animator>().Play("calm");
        }
        
        if (isHero && Input.GetKeyDown(KeyCode.R))
        {
            obj.transform.GetChild(3).gameObject.SetActive(true);
            obj.transform.GetChild(4).gameObject.SetActive(true);
        }

        if (!isHero)
        {
            obj.transform.GetChild(3).gameObject.SetActive(false);
            obj.transform.GetChild(4).gameObject.SetActive(false);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject) isHero = true;
        if(collider.gameObject == Hero.Instance.gameObject && obj.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("shine")
        )
        {
            obj.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (collider.gameObject == Hero.Instance.gameObject && obj.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("calm"))
        {
            obj.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject) isHero = false;
        if(collider.gameObject == Hero.Instance.gameObject)
        {
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    
    
}
