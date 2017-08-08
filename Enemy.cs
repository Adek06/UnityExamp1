using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Transform player;
    private Vector2 targetPos;
    public int soomthing = 6;
    public float restTime = 1f;
    public float restTimer = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = transform.position;
    }

    private void Update()
    {
        GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position,targetPos,soomthing*Time.deltaTime));
        restTimer += Time.deltaTime;
        if (restTimer>restTime) {
            Move();
        }
    }

    public void Move()
    {
        Vector2 offset = player.position - transform.position;
        if (offset.magnitude < 1.1f)
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
        else {
            if (Mathf.Abs(offset.y) > Mathf.Abs(offset.x)){
                if (offset.y < 0)
                {
                    MoveTo(0, -1);
                }
                else {
                    MoveTo(0,1);
                }
            }
            else
            {
                if (offset.x < 0)
                {
                    MoveTo(-1, 0);
                }
                else {
                    MoveTo(1,0);
                }
            }
        }
        restTimer = 0;
    }

    private void MoveTo(float x,float y) {
        GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(x, y));
        GetComponent<BoxCollider2D>().enabled = true;
        if (hit.transform == null)
        {
            targetPos += new Vector2(x, y);
        }

    }
}
