using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAtStartOfScene : MonoBehaviour
{
    public CharacterDialogue dialogue;
    private void Start()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);

    }
}
