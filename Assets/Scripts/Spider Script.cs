using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float speed = 2.5f;
    private Vector2 moveDirection;

    private bool canMove;

    private string coroutineName = "ChangeDirection";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        moveDirection = Vector2.down;
        StartCoroutine(coroutineName);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == MyTag.PlayerTag)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTag.BulletTag)
        {
            Dead();
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(2f);
        
        if(moveDirection == Vector2.down)
        {
            moveDirection = Vector2.up;
        }
        else
        {
            moveDirection = Vector2.down;
        }

        StartCoroutine(coroutineName);
    }

    private void Dead()
    {
        anim.Play("Spider Dead");
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, 3f);
    }
}
