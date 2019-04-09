using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

    public static StateManager instance = null;
    public PlayerController player;
    public Transform[] spawnPoints;

    private CameraController camera;
    private Canvas canvas;
    private GameObject dialogueBox;
    private Text npcName;
    private Text dialogueText;
    private Animator animator; 


    // Initialization
    // ____________________
    private void Awake()
    {
        //If StateManager isn't yet created, create it
        if (instance == null) {
            instance = this;
        } 

        //If StateManager already exists, destroy this object
        else if (instance != this) {
            Destroy(gameObject);
        }

        //Don't destroy this game object on scene changes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        camera = FindObjectOfType<Camera>().GetComponent<CameraController>();
        animator = GetComponent<Animator>();

        //Get reference to dialogue box; don't destroy upon load
        //dialogueBox = GameObject.Find("Dialogue Box");
        //npcName = dialogueBox.GetComponentsInChildren<Text>()[0];
        //dialogueText = dialogueBox.GetComponentsInChildren<Text>()[1];
        //dialogueBox.SetActive(false);

        DontDestroyOnLoad(canvas);

        startAnimation("title");
    }

    //Load specified level 
    public void loadLevel(string levelToLoad) {
        SceneManager.LoadScene(levelToLoad);
    }

    //Restart the current level
    public void restartLevel() {    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Start dialogue
    //public void startDialogue(Dialogue dialogue, int index) {
    //    Debug.Log("Start Dialogue");

    //    npcName.text = dialogue.name;
    //    dialogueText.text = dialogue.sentences[index];

    //    dialogueBox.SetActive(true);
    //}

    //public void endDialogue() {
    //    dialogueBox.SetActive(false);
    //}

    public void startAnimation(string name) {

        // Turn on animator; stop camera follow
        cameraEndFollow();
        animator.enabled = true;

        // Choose which animation to play
        switch (name) {
            case "title":
                canvas.GetComponent<Animator>().SetTrigger("title");
                break;

            case "intro":
                animator.SetTrigger("intro");
                break;  

            case "fireTransport":
                // Move player to the start of the fire level 
                movePlayer(spawnPoints[0]); 
                break;

        }
    }

    public void cameraStartFollow() {
        animator.enabled = false;
        camera.startFollow();
        canvas.GetComponent<Animator>().SetTrigger("healthIntro");
    }

    public void cameraEndFollow() {
        camera.endFollow();
    }

    public void movePlayer(Transform location) {
        player.transform.position = location.position;
    }
}
