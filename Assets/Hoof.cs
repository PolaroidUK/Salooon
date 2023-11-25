using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoof : MonoBehaviour
{
    [SerializeField] private CardHolder[] holders;
    public void GiveCards(CardVisual c1, CardVisual c2, CardVisual c3)
    {
        holders[0].SetCard(c1);
        holders[1].SetCard(c2);
        holders[2].SetCard(c3);
    }

    public CardVisual GetCard(int currentPlayerSelectedCard)
    {
        return holders[currentPlayerSelectedCard - 1].GetCard();
    }

    public CardHolder GetHolder(int currentPlayerSelectedCard)
    {
        return holders[currentPlayerSelectedCard - 1];
    }
}
