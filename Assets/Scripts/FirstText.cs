using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstText : MonoBehaviour
{

    private GameObject obj;
    private bool isHero = false;
    void Start()
    {
        obj = this.gameObject;
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.transform.GetChild(3).gameObject.SetActive(false);
    }

    void Update()
    {
        if (isHero) obj.transform.GetChild(1).gameObject.SetActive(true);
        else
        {
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
            obj.transform.GetChild(3).gameObject.SetActive(false);
        }

        if (isHero && Input.GetKeyDown(KeyCode.R))
        {
            obj.transform.GetChild(2).gameObject.SetActive(true);
            obj.transform.GetChild(3).gameObject.SetActive(true);
        }
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
