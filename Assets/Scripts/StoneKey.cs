using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneKey : MonoBehaviour
{
    private Animator anim;
    private int lives;
    private GameObject obj;

    void Start()
    {
        anim = GetComponent<Animator>();
        lives = Hero.Instance.lives;
        obj = this.gameObject;
        
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(false);
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
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
        if(collider.gameObject == Hero.Instance.gameObject)
        {
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    
    
}
