using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTag.PlayerTag)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }

        if(collision.tag == MyTag.PlayerTag || collision.tag == MyTag.BulletTag)
        {
            Destroy(gameObject);
        }
    }
}
