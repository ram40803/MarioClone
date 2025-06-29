using System.Collections;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool canMove;
    private bool attacked;
    private float startX, endX;

    private float speed;

    public GameObject birdStone;
    public Transform birdStonePos;

    public LayerMask playerLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        attacked = false;
        speed = 2.5f;
        startX = transform.position.x - 6f;
        endX = transform.position.x + 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }

        if (!attacked)
        {
            RaycastHit2D hit = Physics2D.Raycast(birdStonePos.position, Vector2.down, Mathf.Infinity, playerLayer);

            if(hit.collider != null && hit.collider.tag == MyTag.PlayerTag)
            {
                Attack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTag.BulletTag)
        {
            BirdDead();
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);

        // this code for change direction
        if (transform.position.x < startX || transform.position.x > endX)
        {
            speed *= -1;

            Vector2 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;
        }
    }

    private void Attack()
    {
        attacked = true;
        anim.Play("Bird Fly");
        Instantiate(birdStone, birdStonePos.position, Quaternion.identity);
    }

    private void BirdDead()
    {
        canMove = false;
        anim.Play("Bird Dead");
        GetComponent<Collider2D>().isTrigger = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, 5f);
    }
}