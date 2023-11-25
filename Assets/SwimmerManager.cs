using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwimmerManager : MonoBehaviour
{
    public int currentPlayerSelectedCard,currentTableSelectedCard;
    [SerializeField] private int currentTurn;
    [SerializeField] public bool firstRound =true;
    
    [SerializeField] private bool lastRound;
    [SerializeField] private bool gameEnded;

    [SerializeField] private float aiTimer;
    [SerializeField] private float aiTurnTime = 1;
    [SerializeField] private List<CardVisual> deck;
    [SerializeField] private Dog[] dogs = new Dog[3];
    [SerializeField] private Table table;
    [SerializeField] private Hoof hoof;
    [SerializeField] private TextMeshProUGUI playerScore;
    
    public event Action MoveCards;
    public event Action UpdateHolders;
    public void SelectPlayerCard(int card)
    {
        currentPlayerSelectedCard = card;
    }
    public void SelectTableCard(int card)
    {
        currentTableSelectedCard = card;
    }

    private void Start()
    {
        foreach (CardVisual card in deck)
        {
            card.SetPosition(transform.position);
            card.Hide();
        }
        foreach (Dog dog in dogs)
        {
            dog.GiveCards(Draw(),Draw(),Draw());
        }
        table.GiveCards(Draw(),Draw(),Draw());
        hoof.GiveCards(Draw(),Draw(),Draw());
        MoveCards?.Invoke();
    }
    
    public CardVisual Draw()
    {
        CardVisual drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }
    public void SwapAll()
    {
        if (currentTurn !=0 || gameEnded)
        {
            return;
        }
        print("Swapping All");
        
        table.SwapCard(1,hoof.GetHolder(1));
        table.SwapCard(2,hoof.GetHolder(2));
        table.SwapCard(3,hoof.GetHolder(3));
        
        EndTurn();
    }

    public void Skip()
    {
        if (currentTurn !=0|| gameEnded)
        {
            return;
        }
        print("Skipping");
        EndTurn();
    }
    public void Knock()
    {
        if (currentTurn !=0|| gameEnded|| firstRound)
        {
            return;
        }
        print("Knocking");
        lastRound = true;
        EndTurn();
    }
    public void Swap()
    {
        if (currentTurn !=0|| gameEnded)
        {
            return;
        }
        if (HasSelectedTwoCards())
        {
            return;
        }
        print("Swapping");
        table.SwapCard(currentTableSelectedCard,hoof.GetHolder(currentPlayerSelectedCard));
        EndTurn();
    }

    private void EndTurn()
    {
        currentPlayerSelectedCard = 0;
        currentTableSelectedCard = 0;
        currentTurn = 1;
        UpdateHolders?.Invoke();
        MoveCards?.Invoke();
    }

    public float CalculateScore(CardVisual c1,CardVisual c2, CardVisual c3)
    {
        if (c1.face==c2.face&&c1.face==c3.face)
        {
            if (c1.face ==Face.Ace)
            {
                return 33f;
            }
            else
            {
                return 30.5f;
            }
        }

        if (c1.suit == c2.suit && c1.suit == c3.suit)
        {
            return FaceConvert(c1.face) + FaceConvert(c2.face) + FaceConvert(c3.face);
        }

        if (c1.suit ==c2.suit)
        {
            return FaceConvert(c1.face) + FaceConvert(c2.face);
        }
        if (c1.suit ==c3.suit)
        {
            return FaceConvert(c1.face) + FaceConvert(c3.face);
        }
        if (c2.suit ==c3.suit)
        {
            return FaceConvert(c2.face) + FaceConvert(c3.face);
        }

        float highestValue = FaceConvert(c1.face);
        highestValue = Mathf.Max(highestValue, FaceConvert(c2.face));
        highestValue = Mathf.Max(highestValue, FaceConvert(c3.face));
        return highestValue;
    }
    
    public float FaceConvert(Face face)
    {
        switch (face)
        {
            case Face.Seven:
                return 7;
            case Face.Eight:
                return 8;
            case Face.Nine:
                return 9;
            case Face.Ten:
                return 10;
            case Face.Jack:
                return 10;
            case Face.Queen:
                return 10; 
            case Face.King:
                return 10;
            case Face.Ace:
                return 11;
            default:
                return 7;
        }
    }

    private void Update()
    {
        if (currentTurn!=0 && !gameEnded)
        {
            aiTimer += Time.deltaTime;
            if (aiTimer>aiTurnTime)
            {
                aiTimer = 0;
                switch (currentTurn)
                {
                    case 1:
                        dogs[0].MakeMove(table);
                        currentTurn = 2;
                        UpdateHolders?.Invoke();
                        MoveCards?.Invoke();
                        break;
                    case 2:
                        currentTurn = 3;
                        UpdateHolders?.Invoke();
                        MoveCards?.Invoke();
                        break;
                    case 3:
                        currentTurn = 0;
                        UpdateHolders?.Invoke();
                        MoveCards?.Invoke();
                        firstRound = false;
                        if (lastRound)
                        {
                            EndRound();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void EndRound()
    {
        gameEnded = true;
        currentTurn = 0;
        foreach (Dog dog in dogs)
        {
            dog.ShowCards();
        }

        playerScore.text = "Score: " + CalculateScore(hoof.GetCard(1),hoof.GetCard(2),hoof.GetCard(3));
    }

    public void SelectCard(bool isHoof,int cardId)
    {
        if (isHoof)
        {
            currentPlayerSelectedCard = cardId;
        }
        else
        {
            currentTableSelectedCard = cardId;
        }
        UpdateHolders?.Invoke();
    }

    public bool HasSelectedTwoCards()
    {
        return currentPlayerSelectedCard == 0 || currentTableSelectedCard == 0;
    }
}
public enum Suit
{
    Spades,Hearts,Clubs,Diamonds
}
public enum Face
{
    Seven,Eight,Nine,Ten,Jack,Queen,King,Ace
}