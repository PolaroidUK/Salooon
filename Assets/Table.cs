using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private CardHolder[] holders;
    public void GiveCards(Card c1, Card c2, Card c3)
    {
        holders[0].SetCard(c1);
        holders[1].SetCard(c2);
        holders[2].SetCard(c3);
    }

    public void SwapCard(int id, CardHolder holder)
    {
        Card card = holders[id - 1].GetCard();
        holders[id - 1].SetCard(holder.GetCard());
        holder.SetCard(card);
    }

    public Card GetCard(int i)
    {
        return holders[i].GetCard();
    }
    public void SetCard(int i, Card card)
    {
        holders[i].SetCard(card);
    }

    public void DiscardCards(GameObject discardPile)
    {
        holders[0].DiscardCard(discardPile.transform.position);
        holders[1].DiscardCard(discardPile.transform.position);
        holders[2].DiscardCard(discardPile.transform.position);
    }
}
