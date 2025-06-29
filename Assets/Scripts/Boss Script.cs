using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    private int helth;

    public GameObject bossStone;
    private Transform stonePos;

    private bool isDeat;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        stonePos = transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        isDeat = false;
        anim.SetBool("isDeat", false);
        anim.SetBool("isAttack", false);
        helth = 10;
        StartCoroutine(PlayAttack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTag.BulletTag)
        {
            helth--;

            print(helth);

            if(helth <= 0)
            {
                anim.SetBool("isDeat", true);
                audioSource.Play();
            }
        }
    }

    private void PlayIdel()
    {
        anim.SetBool("isAttack", false);
    }

    private void Attack()
    {
        GameObject stone = Instantiate(bossStone, stonePos.position, Quaternion.identity);
        stone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Random.Range(500f, 700f));
    }

    private void Deat()
    {
        Destroy(gameObject, 1f);
        GameObject.Find("ScenesManager").GetComponent<ScenesManager>().GameComplete();
    }

    private IEnumerator PlayAttack()
    {
        while (!isDeat)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            anim.SetBool("isAttack", true);
        }
    }
}
