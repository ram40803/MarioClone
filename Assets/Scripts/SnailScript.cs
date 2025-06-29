using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool moveLeft;
    private bool canMove;
    private bool stunned;

    public Transform topCheck, bottomCheck, leftCheck, rightCheck;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
        stunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
        if (canMove)
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    private void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCheck.position, Vector2.left, 0.2f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCheck.position, Vector2.right, 0.2f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCheck.position, 0.05f, playerLayer);

        if(topHit != null)
        {
            if (!stunned && topHit.gameObject.tag == MyTag.PlayerTag)
            {
                topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7);

                Stunned();
            }
        }

        if(leftHit.collider != null)
        {
            if(leftHit.collider.gameObject.tag == MyTag.PlayerTag)
            {
                if (stunned)
                {
                    rb.velocity = new Vector2(15f, rb.velocity.y);
                    Destroy(gameObject, 1f);
                }
                else
                {
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
            }
            
        }

        if(rightHit.collider != null)
        {
            if(rightHit.collider.gameObject.tag == MyTag.PlayerTag)
            {
                if (stunned)
                {
                    rb.velocity = new Vector2(-15f, rb.velocity.y);
                    Destroy(gameObject, 1f);
                }
                else 
                {
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
            }
        }

        if (!Physics2D.Raycast(bottomCheck.transform.position, Vector2.down, 0.2f, groundLayer))
        {
            if (canMove)
            {
                ChangedDirection();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTag.BulletTag)
        {
            if (!stunned)
            {
                Stunned();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void ChangedDirection()
    {
        moveLeft = !moveLeft;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;

        //this code for leftCheck remain in left and rightCheck remain in right after it Changed Direction
        Vector2 temp = rightCheck.transform.position;
        rightCheck.transform.position = leftCheck.transform.position;
        leftCheck.transform.position = temp;
    }

    private void Stunned()
    {
        canMove = false;
        stunned = true;

        rb.velocity = Vector2.zero;

        if (tag == MyTag.BeetleTag)
        {
            anim.Play("Beetle Stunned");
            Destroy(gameObject, 1f);
        }
        else
        {
            anim.Play("Snail Stunned");
        }
    }
}
