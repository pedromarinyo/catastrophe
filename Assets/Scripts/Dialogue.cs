using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;             // Name of NPC who's speaking 

    [TextArea(3, 10)]
    public string[] sentences;      // Sentences the NPC is speaking
}
