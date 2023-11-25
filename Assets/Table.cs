using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private CardHolder[] holders;
    public void GiveCards(CardVisual c1, CardVisual c2, CardVisual c3)
    {
        holders[0].SetCard(c1);
        holders[1].SetCard(c2);
        holders[2].SetCard(c3);
    }

    public void SwapCard(int id, CardHolder holder)
    {
        CardVisual card = holders[id - 1].GetCard();
        holders[id - 1].SetCard(holder.GetCard());
        holder.SetCard(card);
    }

    public CardVisual GetCard(int i)
    {
        return holders[i].GetCard();
    }
    public void SetCard(int i, CardVisual card)
    {
        holders[i].SetCard(card);
    }
}
