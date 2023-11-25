using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : MonoBehaviour
{
    HoverHandler hoverHandler;
    public string hoverText;
    public CharacterDialogue dialogueWhenClicked;
    DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        hoverHandler = FindObjectOfType<HoverHandler>();
    }

    private void OnMouseUp()
    {
        dialogueManager.StartDialogue(dialogueWhenClicked);
        hoverHandler.OnHoverExit();
    }

    void OnMouseOver()
    {
        if (!dialogueManager.CurrentlyShowingUI())
            hoverHandler.OnHoverOver(hoverText);
    }

    void OnMouseExit()
    {
        if (!dialogueManager.CurrentlyShowingUI())
            hoverHandler.OnHoverExit();
    }
}