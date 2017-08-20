using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float h, v;

    public float smoothing = 6f;
    public float restTime = 0.19f;
    public float restTimer = 0;
    public AudioClip foot1, foot2;
    public AudioClip chop1, chop2;
    public AudioClip damage1, damage2;
    public AudioClip fruit1,fruit2,soda1,soda2;
    private Button btnUp;
    private Button btnDown;
    private Button btnLeft;
    private Button btnRight;

    public Vector2 targetPos;// = new Vector2(1, 1);

    // Use this for initialization
    void Awake()
    {
        targetPos = transform.position;
       // btnUp = GameObject.Find("up").GetComponent<Button>();
        //btnDown = GameObject.Find("down").GetComponent<Button>();
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

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            h = Analogy.Instance.Drage().x;
            v = Analogy.Instance.Drage().y;
            if (Mathf.Abs(h) > Mathf.Abs(v))
            {
                v = 0;
                if (h > 0) { h = 1; }
                else if (h < 0) { h = -1; }
            }
            else if(Mathf.Abs(h) < Mathf.Abs(v)) {
                h = 0;
                if (v > 0) { v = 1; }
                else if (v < 0) { v = -1; }
            }
            if (h != 0 )
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
            GameManager.Instance.ReduceFood(1);
           // AudioManager.Instance.RandomPlay(foot1,foot2);
        }
        else {
            switch (hit.collider.tag) {
                case "Wall" :
                    GetComponent<Animator>().SetTrigger("Attack");
                    AudioManager.Instance.RandomPlay(chop1 ,chop2);
                    GameManager.Instance.ReduceFood(10);
                    hit.collider.SendMessage("TakeDamage");
                    break;
                case "OutWall":
                    break;
                case "Food":
                    EatFood(10,new Vector2(h,v),hit);
                    AudioManager.Instance.RandomPlay(fruit1,fruit2);
                    break;
                case "Soda":
                    EatFood(20, new Vector2(h, v), hit);
                    AudioManager.Instance.RandomPlay(soda1 ,soda2);
                    break;
                case "Exit":
                    GameManager.Instance.exitOut();
                    break;
            }
        }
        restTimer = 0;
    }

    private void EatFood(int count,Vector2 Pos,RaycastHit2D hit) {
        GameManager.Instance.AddFood(count);
        targetPos += Pos;
        Destroy(hit.transform.gameObject);
    }

    public void TakeDamage() {
        AudioManager.Instance.RandomPlay(damage1,damage2);
        GameManager.Instance.ReduceFood(30);
        GetComponent<Animator>().SetTrigger("Damage");
        
    }
}
