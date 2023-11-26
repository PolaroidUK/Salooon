using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : MonoBehaviour
{
    HoverHandler hoverHandler;
    public string hoverText;
    public CharacterDialogue dialogueWhenClicked;
    DialogueManager dialogueManager;
    InventorySystem inventorySystem;
    public Item itemThatTriggers;

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        hoverHandler = FindObjectOfType<HoverHandler>();
    }

    private void OnMouseUp()
    {
        Item item = inventorySystem.GetSelectedItem();
        if (!item)
        {
            dialogueManager.StartDialogue(dialogueWhenClicked);
            hoverHandler.OnHoverExit();
        }
        else
        {
            if (itemThatTriggers == item)
            {
                inventorySystem.RemoveItem(item);
                print("wow thanks!");
            }
            else
            {
                print("no thanks : (");
            }
        }
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