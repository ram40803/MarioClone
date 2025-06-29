using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlocksScript : MonoBehaviour
{
    private Transform bottomCollisionPos;

    private Animator anim;
    public float animationSpeed = 3.0f;

    public LayerMask playerLayer;

    private Vector3 moveDirection;
    private Vector3 originPosition;
    private Vector3 animPosition;
    private bool canAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bottomCollisionPos = transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        canAnim = true;

        moveDirection = Vector3.up;

        originPosition = transform.position;
        animPosition = transform.position + Vector3.up * 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAnim)
        {
            CheckForCollision();
        }
    }

    private void CheckForCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(bottomCollisionPos.position, 0.1f, playerLayer);

        if (hit != null && hit.tag == MyTag.PlayerTag)
        {
            anim.Play("Block Idel");
            canAnim = false;

            GameObject.FindGameObjectWithTag(MyTag.PlayerTag).GetComponent<ScoreScript>().IncrimentScore(1);
            StartCoroutine(AnimateUpDown());
        }
    }

    private IEnumerator AnimateUpDown()
    {
        yield return null;

        transform.Translate(moveDirection * Time.deltaTime * animationSpeed);

        if (transform.position.y >= animPosition.y)
        {
            moveDirection = Vector3.down;
        }
        else if (transform.position.y <= originPosition.y)
        {
            transform.position = originPosition;
            StopCoroutine(AnimateUpDown());
        }

        StartCoroutine(AnimateUpDown());
    }
}
