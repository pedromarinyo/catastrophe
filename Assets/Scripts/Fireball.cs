using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed = 20f;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        // Getting reference to rigidbody2d
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

            // Trigger hit animation
            animator.SetTrigger("hit");
            rb.velocity = Vector3.zero;
        }
    }

    private void destroyFireball() {
        Destroy(gameObject);
    }

}
