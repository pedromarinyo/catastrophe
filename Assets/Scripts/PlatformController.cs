using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public float allowence = 5f;
    int destPoint;


    // Use this for initialization
    void Start()
    {
        // Set first target
        UpdateTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // Update this position
        Vector3 thisPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Distance between current position and next position < alloence
        if (Vector3.Distance(thisPos, points[destPoint].position) < allowence)
        {
            UpdateTarget();
        }

        transform.position = Vector3.Lerp(transform.position, points[destPoint].position, speed * Time.deltaTime);
    }

    void UpdateTarget()
    {
        if (points.Length == 0)
        {
            return;
        }
        transform.position = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}