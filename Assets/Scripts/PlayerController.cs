using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public CharacterController2D controller;    // Character move controller
    public float runSpeed = 20f;                // Character's run speed

    private bool jump = false;
    private float horizontalMove = 0f;
    private Animator animator; 


    private void Start() {
        // Getting a reference to this object's Animator
        animator = GetComponent<Animator>();
    }

    private void Update() {
        // Calculating horizontal movemnet 
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        // Checking if "jump" is pressed.
        if (Input.GetButtonDown("Jump")) { 
            jump = true;
        }
    }

    void FixedUpdate () 
    {
        // Send move data to character controller 
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
    }

    // Interations with Enemeies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collisions with obstacles
        if (collision.gameObject.tag == "Obstacle")
        {
            //// Calculate direction between obstacle and player
            //Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);

            //// Reverse the calculated direction
            //dir = -dir.normalized;

            //// Calclate enemy repel force
            //float repelForce = collision.gameObject.GetComponent<ObstacleController>().repelForce; 

            //// Apply a force to the player in the calculated direction
            //gameObject.GetComponent<Rigidbody2D>().AddForce(dir * repelForce);

            //// Trigger animator into damage animation
            ////animator.SetTrigger("damage");

            // Restart level
            StateManager.instance.restartLevel();

        }    
    }

    // Interactions with NPCs
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" && Input.GetButtonDown("Fire1")) {
            collision.gameObject.GetComponent<DialogueTrigger>().triggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        StateManager.instance.endDialogue();
    }
}


