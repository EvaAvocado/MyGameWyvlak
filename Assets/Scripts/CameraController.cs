using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            transform.position = Vector3.Lerp(transform.position, pos, 3 * Time.deltaTime);
        }

    }
}
