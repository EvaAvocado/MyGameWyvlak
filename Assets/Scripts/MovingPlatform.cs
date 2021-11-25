using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Transform pos1, pos2;
    private float speed = 3f;

    private Rigidbody2D rb;
    [SerializeField] private Transform startPos;

    Vector2 nextPos;
    Vector2 prevPos;
    private bool moveToFirst = false;


    void Awake()
    {
        //rb = GetComponentInChildren<Rigidbody2D>();
        nextPos = pos2.position;
        prevPos = (Vector2)transform.position;
        moveToFirst = false;
    }

    void LateUpdate()
    {
        if (Vector2.Distance(transform.position, nextPos) < Time.deltaTime * 2 * speed)
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

        transform.position = Vector2.MoveTowards(transform.position, nextPos,
            speed * Time.deltaTime);
        prevPos = (Vector2)transform.position;
        //rb.velocity = (nextPos - prevPos).normalized * speed;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
