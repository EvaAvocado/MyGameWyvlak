using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewLevel : MonoBehaviour
{
    // Перейти на другую сцену
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject && SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level1");
        }
        else if (collision.gameObject == Hero.Instance.gameObject && SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }

        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
