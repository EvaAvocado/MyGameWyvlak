using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingCloud : MonoBehaviour
{
    [SerializeField] private Transform pos1, pos2;
    private float speed = 3f;

    private Rigidbody2D rb;
    [SerializeField] private Transform startPos;

    Vector2 nextPos;
    private bool moveToFirst = false;

    private bool movingRight = true;
    private bool moving = false;


    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        nextPos = pos2.position;
        moveToFirst = false;
    }


    void Update()
    {
        if (moving) Go();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            moving = true;
        }
    }

   /* private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            moving = false;
        }
    }*/

    private void Go()
    {
        if ((nextPos - (Vector2)rb.position).magnitude < 1.1f * Time.deltaTime * speed)
        {
            if (moveToFirst)
            {
                moveToFirst = false;
                nextPos = pos2.position;
            }
            else
            {
                moveToFirst = true;
                nextPos = pos1.position;
            }
        }

        rb.velocity = (nextPos - (Vector2)rb.position).normalized * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
