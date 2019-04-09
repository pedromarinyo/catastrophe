using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public bool follow = false;
    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (follow) 
        {
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            //transform.LookAt(target);
        }
    }

    // Make camera follow the player
    public void startFollow() {
        follow = true;
    }

    // Make camera stop following the player
    public void endFollow() {
        follow = false;
    }
}
