using System.Collections;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;
    private int jumpCount;
    private int totalJump;

    private bool leftDriction;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 0;
        totalJump = 3;
        leftDriction = true;

        StartCoroutine(JumpStart());
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpCount == totalJump)
        {
            ChangeDirection();
            jumpCount = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTag.PlayerTag)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    private void JumpEnd()
    {
        anim.SetBool("Jump", false);

        Vector2 temp = transform.position;
        transform.localPosition = Vector2.zero;
        transform.parent.position = temp;

        jumpCount++;
    }

    private IEnumerator JumpStart()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Jump", true);
        StartCoroutine(JumpStart());
    }

    private void ChangeDirection()
    {
        leftDriction = !leftDriction;
        anim.SetBool("Left", leftDriction);
    }
}
