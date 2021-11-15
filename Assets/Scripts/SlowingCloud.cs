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
    Vector2 prevPos;
    private bool moveToFirst = false;

    private bool movingRight = true;
    private bool moving = false;


    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        nextPos = pos2.position;
        prevPos = (Vector2)rb.position;
        moveToFirst = false;
    }


    void Update()
    {
        Go();
        //if (moving) Go();
    }

    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            moving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            moving = false;
            rb.velocity = Vector2.zero;
        }
    }*/

    private void Go()
    {
        if ((nextPos - prevPos).magnitude < 1.1f * Time.deltaTime * speed)
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

        prevPos = (Vector2)rb.position;
        rb.velocity = (nextPos - prevPos).normalized * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
