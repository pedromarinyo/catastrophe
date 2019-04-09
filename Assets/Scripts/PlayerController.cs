using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public CharacterController2D controller;    // Character move controller
    public float runSpeed = 20f;                // Character's run speed

    public GameObject fireball;
    public Transform fireballBounds;

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

        // Checking for fireball
        if (Input.GetButtonDown("Fire1")) {
            Fireball();
        }
    }

    void FixedUpdate () 
    {
        // Send move data to character controller 
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
    }


    private void Fireball() {
        // Spawn fireball at player's location
        GameObject projectile = Instantiate(fireball, fireballBounds.position, transform.rotation);
        Destroy(projectile, 3f);

        // Trigger fireball player
        animator.SetTrigger("attack");
    }

    // Interactions with NPCs
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" && Input.GetButtonDown("Fire1")) {
            collision.gameObject.GetComponent<DialogueTrigger>().triggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // Checking if walking away from an NPC...
        if (collision.tag == "NPC"){

            // End dialogue
            //StateManager.instance.endDialogue();
        }

    }
}


