using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject obj;
    private Animator anim;
    private bool isHero = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        obj = this.gameObject;
    }
    
    void Update()
    {
        if (isHero && Input.GetKeyDown(KeyCode.E))
        {
            obj.SetActive(false);
            Hero.Instance.haveKey = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject) isHero = true;
        anim.Play("goKey");
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject) isHero = false;
    }
}
