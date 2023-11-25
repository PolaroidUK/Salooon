using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private CardVisual[] paw =  new CardVisual[3];
    public GameObject[] pawAnchors;
    public void GiveCards(CardVisual c1, CardVisual c2, CardVisual c3)
    {
        SetCard(0,c1);
        SetCard(1,c2);
        SetCard(2,c3);
    }
    public void SetCard(int i,CardVisual card)
    {
        paw[i] = card;
        paw[i].SetPosition(pawAnchors[i].transform.position);
        paw[i].Hide();
    }
    public CardVisual GetCard(int i)
    {
        return paw[i];
    }

    public void MakeMove(Table table)
    {
        
    }

    public void ShowCards()
    {
        foreach (CardVisual card in paw)
        {
            card.Show();
        }
    }
}
