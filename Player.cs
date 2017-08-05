using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float smoothing = 6f;
    public float restTime = 0.19f;
    public float restTimer = 0;

    private Vector2 targetPos = new Vector2(1, 1);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));
        restTimer += Time.deltaTime;
        if (restTimer < restTime)
        {
            return;
        }
        else
        {

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            if (h != 0)
            {
                v = 0;
            }
            if (h != 0 || v != 0)
            {
                deteHinder(h, v);
            }

        }
    }

    private void deteHinder(float h, float v)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
        GetComponent<BoxCollider2D>().enabled = true;

        if (hit.transform == null)
        {
            targetPos += new Vector2(h, v);
            restTimer = 0;
        }
        else if (hit.collider.tag == "Wall")
        {
            GetComponent<Animator>().SetTrigger("Attack");
            hit.collider.SendMessage("TakeDamage");
            restTimer = 0;
        }
        else if (hit.collider.tag == "OutWall")
        {
            restTimer = 0;
        }
    }
}
