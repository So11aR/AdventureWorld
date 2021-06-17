using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rb;
    float timer;
    int direction = 1;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        anim = GetComponent<Animator>();
    }

    IEnumerator Broken()
    {
        enabled = false;
        anim.enabled = false;
        yield return new WaitForSeconds(3);
        enabled = true;
        anim.enabled = true;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if(player != null)
        {
            player.ChangeHp(-5);
        }

        if(other.gameObject.tag == "Bullet")
        {
            StartCoroutine(Broken());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rb.position;
        if(vertical) 
        {
            anim.SetFloat("Move X", 0);
            anim.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            anim.SetFloat("Move X", direction);
            anim.SetFloat("Move Y", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        
        rb.MovePosition(position);
    }
}
