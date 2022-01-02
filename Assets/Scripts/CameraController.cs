using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;


    private void Awake()
    {
        if (!player) player = FindObjectOfType<Hero>().transform;
    }


    void Update()
    {
        //получение координат игрока и перемещение туда камеры
        if (player)
        {
            pos = player.position;
            pos.z = -5f;
            pos.y += 0.2f;
            Vector3 tmpPos = Vector3.Lerp(transform.position, pos, 3 * Time.deltaTime);
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                transform.position = new Vector3(
                    Mathf.Clamp(tmpPos.x, -161, -110),
                    Mathf.Clamp(tmpPos.y, -6, 21),
                    tmpPos.z
                );
            }
            else transform.position = tmpPos;
        }
    }
    
}
