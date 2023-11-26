using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    
    private SwimmerManager manager;

    public int cardHolderID;
    [SerializeField] private bool isHoof;
    
    private Card currentCard;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<SwimmerManager>();
        manager.UpdateHolders += UpdateSprite;
    }
   
    private void OnMouseDown()
    {
        manager.SelectCard(isHoof,cardHolderID);
    }

    public void UpdateSprite()
    {
        if (isHoof)
        {
            spriteRenderer.enabled = manager.currentPlayerSelectedCard== cardHolderID;
        }
        else
        {
            
            spriteRenderer.enabled = manager.currentTableSelectedCard== cardHolderID;
        }
    }

    public void SetCard(Card c1)
    {
        currentCard = c1;
        currentCard.SetPosition(transform.position);
        currentCard.Show();
    }

    public Card GetCard()
    {
        return currentCard;
    }

    public void DiscardCard(Vector3 discardPile)
    {
        currentCard.SetPosition(discardPile);
    }
}
