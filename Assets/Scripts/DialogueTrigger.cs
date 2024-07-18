using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {   //if(GameHandler.emptyCursor == true)
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
