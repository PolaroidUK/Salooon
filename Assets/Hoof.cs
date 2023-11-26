using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoof : MonoBehaviour
{
    [SerializeField] private CardHolder[] holders;
    [SerializeField] private int lives =4;
    [SerializeField] private GameObject[] liveSprites;
    
    public void GiveCards(Card c1, Card c2, Card c3)
    {
        holders[0].SetCard(c1);
        holders[1].SetCard(c2);
        holders[2].SetCard(c3);
    }

    public Card GetCard(int currentPlayerSelectedCard)
    {
        return holders[currentPlayerSelectedCard - 1].GetCard();
    }

    public CardHolder GetHolder(int currentPlayerSelectedCard)
    {
        return holders[currentPlayerSelectedCard - 1];
    }

    public void TakeLife()
    {
        lives--;
        
        UpdateLivesSprites();
    }
    public void UpdateLivesSprites()
    {
        for (var i = 0; i < liveSprites.Length; i++)
        {
            liveSprites[i].SetActive(lives > i);
        }
    }
}
