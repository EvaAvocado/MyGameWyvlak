using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private GameObject obj;
    private bool isHero = false;
    void Start()
    {
        obj = this.gameObject;
    }
    void Update()
    {
        if (isHero && Hero.Instance.haveKey && Input.GetKeyDown(KeyCode.E) && SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level1");
        }
        if (isHero && Hero.Instance.haveKey && Input.GetKeyDown(KeyCode.E) && SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
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
