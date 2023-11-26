using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private Card[] paw =  new Card[3];
    public GameObject[] pawAnchors;
    [SerializeField] private int lives = 4 ;
    
    [SerializeField] private GameObject[] liveSprites;
    [SerializeField] private int dogID = 1;
    [SerializeField] private bool outOfGame;
    
    public void GiveCards(Card c1, Card c2, Card c3)
    {
        SetCard(0,c1);
        SetCard(1,c2);
        SetCard(2,c3);
    }
    public void SetCard(int i,Card card)
    {
        paw[i] = card;
        paw[i].SetPosition(pawAnchors[i].transform.position);
        paw[i].Hide();
    }
    public Card GetCard(int i)
    {
        return paw[i];
    }

    public int MakeMove(Table table)
    {
        Vector2 bestMove;
        float bestScore;
        switch (dogID)
        {
            case 1:
                if (Random.value>0.5f)
                {
                    SwapCards(Random.Range(0,3),Random.Range(0,3),table);
                    return 1;
                }

                return 0;
            case 2:
                
                bestMove = Vector2.left;
                bestScore = SwimmerManager.CalculateScore(paw[0],paw[1],paw[2]);
                if (bestScore>29)
                {
                    return 2;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bestScore<SwimmerManager.CalculateScore(paw[0],paw[1],table.GetCard(i)))
                    {
                        bestMove = new Vector2(2, i);
                        bestScore = SwimmerManager.CalculateScore(paw[0], paw[1], table.GetCard(i));
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bestScore<SwimmerManager.CalculateScore(paw[0],paw[2],table.GetCard(i)))
                    {
                        bestMove = new Vector2(1, i);
                        bestScore = SwimmerManager.CalculateScore(paw[0], paw[2], table.GetCard(i));
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bestScore<SwimmerManager.CalculateScore(paw[2],paw[1],table.GetCard(i)))
                    {
                        bestMove = new Vector2(0, i);
                        bestScore = SwimmerManager.CalculateScore(paw[2], paw[1], table.GetCard(i));
                    }
                }
                if (bestMove != Vector2.left)
                {
                    SwapCards((int)bestMove.x,(int)bestMove.y,table);
                    return 1;
                }
                return 0;
            case 3:
                bestMove = Vector2.left;
                bestScore = SwimmerManager.CalculateScore(paw[0],paw[1],paw[2]);
                if (bestScore>27)
                {
                    return 2;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bestScore<SwimmerManager.CalculateScore(paw[0],paw[1],table.GetCard(i)))
                    {
                        bestMove = new Vector2(2, i);
                        bestScore = SwimmerManager.CalculateScore(paw[0], paw[1], table.GetCard(i));
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bestScore<SwimmerManager.CalculateScore(paw[0],paw[2],table.GetCard(i)))
                    {
                        bestMove = new Vector2(1, i);
                        bestScore = SwimmerManager.CalculateScore(paw[0], paw[2], table.GetCard(i));
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bestScore<SwimmerManager.CalculateScore(paw[2],paw[1],table.GetCard(i)))
                    {
                        bestMove = new Vector2(0, i);
                        bestScore = SwimmerManager.CalculateScore(paw[2], paw[1], table.GetCard(i));
                    }
                }
                if (bestMove != Vector2.left)
                {
                    SwapCards((int)bestMove.x,(int)bestMove.y,table);
                    return 1;
                }
                return 0;
        }
        return 0;
    }

    private void SwapCards(int d,int t,Table table)
    {
        Card card = table.GetCard(t);
        table.SetCard(t, paw[d]);
        SetCard(d, card);
    }

    public void ShowCards()
    {
        foreach (Card card in paw)
        {
            card.Show();
        }
    }
    public void TakeLife()
    {
        lives--;
        if (lives == 0)
        {
            outOfGame = true;
        }
        UpdateLivesSprites();
    }

    public void UpdateLivesSprites()
    {
        for (var i = 0; i < liveSprites.Length; i++)
        {
            liveSprites[i].SetActive(lives > i);
        }
    }

    public bool IsOutOfGame()
    {
        return outOfGame;
    }
}
