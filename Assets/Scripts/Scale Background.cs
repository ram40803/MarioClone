using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float with = sr.sprite.bounds.size.x;
        float hight = sr.sprite.bounds.size.y;

        float worldHight = Camera.main.orthographicSize * 2f;
        float worldWith = (worldHight / Screen.height) * Screen.width;

        Vector2 tempScale = transform.localScale;
        tempScale.x = worldWith / with;
        tempScale.y = worldHight / hight;

        transform.localScale = tempScale;
    }

}
