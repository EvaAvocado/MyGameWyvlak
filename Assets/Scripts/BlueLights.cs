using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueLights : MonoBehaviour
{
    private GameObject obj;
    private Animator anim;
    private bool isHero = false;
    private bool rightCode = false;
    void Start()
    {
        obj = this.gameObject;
        anim = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
            obj.transform.GetChild(3).gameObject.SetActive(false);
        } else if (SceneManager.GetActiveScene().name == "Level1")
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(10).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            switch (Hero.Instance.lives)
            {
                case 4:
                    obj.transform.GetChild(0).gameObject.SetActive(false);
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(2).gameObject.SetActive(false);
                    obj.transform.GetChild(3).gameObject.SetActive(false);
                    break;
                case 3:
                    obj.transform.GetChild(0).gameObject.SetActive(true);
                    obj.transform.GetChild(1).gameObject.SetActive(false);
                    obj.transform.GetChild(2).gameObject.SetActive(false);
                    obj.transform.GetChild(3).gameObject.SetActive(false);
                    break;
                case 2:
                    obj.transform.GetChild(0).gameObject.SetActive(false);
                    obj.transform.GetChild(1).gameObject.SetActive(false);
                    obj.transform.GetChild(2).gameObject.SetActive(true);
                    obj.transform.GetChild(3).gameObject.SetActive(false);
                    break;
                default:
                    obj.transform.GetChild(0).gameObject.SetActive(false);
                    obj.transform.GetChild(1).gameObject.SetActive(false);
                    obj.transform.GetChild(2).gameObject.SetActive(false);
                    obj.transform.GetChild(3).gameObject.SetActive(true);
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            checkRightCode();
            if (isHero)
            {
                obj.transform.GetChild(0).gameObject.SetActive(true);
                anim.Play("pressE");
            } else obj.transform.GetChild(0).gameObject.SetActive(false);

            if (rightCode)
            {
                isHero = false;
                openBox();
                obj.transform.GetChild(10).gameObject.SetActive(true);
            }
        }
    }

    private void checkRightCode()
    {
        if (obj.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite.name == "blueLight4" &&
            obj.transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>().sprite.name == "blueLight5" &&
            obj.transform.GetChild(7).gameObject.GetComponent<SpriteRenderer>().sprite.name == "blueLight3" &&
            obj.transform.GetChild(8).gameObject.GetComponent<SpriteRenderer>().sprite.name == "blueLight2")
        {
            rightCode = true;
        }
    }

    private void openBox()
    {
        anim.Play("openBox");
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject && !rightCode) isHero = true;
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == Hero.Instance.gameObject && !rightCode) isHero = false;
    }
}
