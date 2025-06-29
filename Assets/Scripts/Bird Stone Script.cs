using UnityEngine;

public class BirdStoneScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTag.PlayerTag)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }

        if(collision.gameObject.tag != MyTag.BirdTag && collision.gameObject.tag != MyTag.MainCameraTag)
        {
            Destroy(gameObject);
        }
    }
}
