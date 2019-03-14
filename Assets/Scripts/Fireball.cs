using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed = 20f;
    private Rigidbody2D rb;

    private void Awake()
    {
        // Getting reference to rigidbody2d
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Interactions with environment and enemies
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If fireball collides with something other than the player...
        if (collision.tag != "Player")
        {        
            // Output the collision object's tag
            Debug.Log(collision.tag);

            // Destroy this fireball
            Destroy(gameObject);
        }
    }
}
