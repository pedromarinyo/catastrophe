using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;   // Holds the character's dialogue
    public int index = 0;       // Tracks which line of dialogue NPC is currently speaking

    public void triggerDialogue(){

        // Send dialogue and index to StateManager
        StateManager.instance.startDialogue(dialogue, index);

        // Increase index by one or reset
        if (index < dialogue.sentences.Length - 1) {
            index++;
        } else { 
            index = 0; 
        }
    }
}
