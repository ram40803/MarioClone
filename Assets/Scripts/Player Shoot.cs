using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<BulletScript>().Speed *= (int)transform.localScale.x;
        Destroy(b, 5f);
    }
}
