using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Animator anim;
    private int speed = 10;

    private bool canMove;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    public int Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != MyTag.PlayerTag && collision.tag != MyTag.MainCameraTag && collision.tag != MyTag.BulletTag)
        {
            anim.Play("Bullet Explode");
            canMove = false;
            Destroy(gameObject, 0.2f);
        }
    }
}
