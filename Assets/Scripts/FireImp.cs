using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireImp : MonoBehaviour
{
    public float speed;

    public Transform rightBound;
    public Transform leftBound;

    public float offset;                  // Offset for checking player height agaiinst Imp's height
    public float repelForce;              // Force to repel player 

    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 rightBoundFixed;
    private Vector3 leftBoundFixed;
    private Vector3 direction = Vector3.left;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Save starting transform value of left and right bounds in world coordinates
        rightBoundFixed = rightBound.position;
        leftBoundFixed = leftBound.position;
    }

    private void Start()
    {
        rb.velocity = direction * speed * 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If fire imp touches left bounds, rotate on y axis
        if (transform.position.x < leftBoundFixed.x)
        {
            direction = Vector3.right;           
        }

        // If fire imp touches right bounds, rotate on y axis
        else if (transform.position.x > rightBoundFixed.x)
        {
            direction = Vector3.left;
        }

        rb.velocity = direction * speed * 10f;
    }

    // Attack and Take Damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If bumpbig into something other than the player, move opposit direction
        if (collision.gameObject.tag != "Player") {
            Switch();
            return;
        }

        // Repel
        Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
        // Reverse the calculated direction
        dir = dir.normalized;
        // Apply a force to the player in the calculated direction
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * repelForce * 100f);

        // Damage
        // If player is above the damage box when triggered, destroy this Fire Imp
        if (collision.gameObject.transform.position.y > gameObject.transform.position.y + offset) {
            rb.velocity = Vector3.zero;
            animator.SetTrigger("damage");
        }

        // Attack
        // Otherwise, Fire Imp attacks and resumes walking in opposit direction
        else {
            animator.SetTrigger("attack");
            Switch();
        }
    }

    // Destroys this Fire Imp
    public void Die() 
    {
        Destroy(gameObject);
    }

    // Switch direction and resume movement
    private void Switch() 
    {
        if (direction == Vector3.right) { direction = Vector3.left; }
        else { direction = Vector3.right; }
        rb.velocity = direction * speed * 10f;
    }

}
