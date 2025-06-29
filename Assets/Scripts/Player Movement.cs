using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 8;
    public int jumpPower = 10;

    private Rigidbody2D myBody;
    private Animator anim;
    private bool isGrounded = true;
    private bool jumped = false;

    public Transform groundChackPosition;
    public LayerMask groundLayer;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();

        if(transform.position.y < -5)
        {
            GameObject.Find("ScenesManager").GetComponent<ScenesManager>().GameOver();
            anim.Play("Player Idel");
        }
    }

    private void FixedUpdate()
    {
        if (!ScenesManager.isGameOver)
        {
            PlayerWalk();
        }
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else if(h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        else
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector2 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundChackPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }
}
