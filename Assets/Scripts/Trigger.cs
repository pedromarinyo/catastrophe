using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string triggerName; // Name of the triggered animation

    // Use state manager to trigger animation
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StateManager.instance.startAnimation(triggerName);
    }


}
