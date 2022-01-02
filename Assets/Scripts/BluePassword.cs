using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePassword : MonoBehaviour
{
    [SerializeField] private Sprite[] code;
    private bool isHero = false;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = code[0];
    }
    
    void Update()
    {
        if (isHero && Input.GetKeyDown(KeyCode.E))
        {
            nextSprite();
        }
    }

    private void nextSprite()
    {
        switch (gameObject.GetComponent<SpriteRenderer>().sprite.name)
        {
            case "blueLight2":
                gameObject.GetComponent<SpriteRenderer>().sprite = code[1];
                break;
            case "blueLight3":
                gameObject.GetComponent<SpriteRenderer>().sprite = code[2];
                break;
            case "blueLight4":
                gameObject.GetComponent<SpriteRenderer>().sprite = code[3];
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = code[0];
                break;
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
