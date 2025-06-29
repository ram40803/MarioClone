using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public float cameraSpeed = 3.0f;
    public float resetSpeed = 5.0f;

    public Bounds cameraBounds;

    private Transform target;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;

    private bool followsPlayer;

    private void Awake()
    {
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds = myCol.bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(MyTag.PlayerTag).transform ;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        followsPlayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followsPlayer)
        {
            Vector3 aheadCameraPos = target.position + Vector3.forward * offsetZ;

            if(aheadCameraPos.x >= transform.position.x)
            {
                Vector3 newCameraPos = Vector3.SmoothDamp(transform.position, aheadCameraPos, ref currentVelocity, cameraSpeed);

                transform.position = new Vector3(newCameraPos.x, transform.position.y, newCameraPos.z);

                lastTargetPosition = target.position;
            }
        }
    }
}
