using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private GameObject obj;
    private bool isHero = false;
    void Start()
    {
        obj = this.gameObject;
        obj.transform.GetChild(0).gameObject.SetActive(false);  
    }

    
    void Update()
    {
        if(isHero && Input.GetKeyDown(KeyCode.E) && !Hero.Instance.haveKey && obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("shine")) {
            obj.transform.GetChild(0).gameObject.SetActive(true);
        }
        if(Hero.Instance.haveKey) obj.transform.GetChild(0).gameObject.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject) isHero = true;
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject) isHero = false;
    }
}
