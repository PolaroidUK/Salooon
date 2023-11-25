using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwimmerManager : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<Card> playerOne;    
    [SerializeField] private List<Card> playerTwo;    
    [SerializeField] private List<Card> playerThree;    
    [SerializeField] private List<Card> playerFour;
    [SerializeField] private List<Card> table;
    [SerializeField] private int currentPlayerSelectedCard,currentTableSelectedCard;
    [SerializeField] private List<PlayerCard> playerCards;
    [SerializeField] private List<PlayerCard> tableCards;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI p2Text, p3Text, p4Text;
    [SerializeField] private int currentTurn;
    [SerializeField] private bool firstRound =true;
    
    [SerializeField] private bool lastRound;
    [SerializeField] private bool gameEnded;

    [SerializeField] private float aiTimer;
    [SerializeField] private float aiTurnTime = 1;
    
    
    
    public void SelectPlayerCard(int card)
    {
        currentPlayerSelectedCard = card;
        UpdateCardVisuals();
    }
    public void SelectTableCard(int card)
    {
        currentTableSelectedCard = card;
        UpdateCardVisuals();
    }
    public void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                _cards.Add(new Card((Suit)i,(Face)j));
            }
        }
        _cards.Sort((a, b)=> 1 - 2 * Random.Range(0, 1));
        playerOne.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerOne.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerOne.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerTwo.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerTwo.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerTwo.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerThree.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerThree.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerThree.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerFour.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerFour.Add(_cards[0]);
        _cards.RemoveAt(0);
        playerFour.Add(_cards[0]);
        _cards.RemoveAt(0);
        table.Add(_cards[0]);
        _cards.RemoveAt(0);
        table.Add(_cards[0]);
        _cards.RemoveAt(0);
        table.Add(_cards[0]);
        _cards.RemoveAt(0);
        
        
        
    }

    private void Start()
    {
        UpdateCardVisuals();
    }

    public void SwapAll()
    {
        if (currentTurn !=0 || gameEnded)
        {
            return;
        }
        (playerOne[0], table[0]) = (table[0], playerOne[0]);
        (playerOne[1], table[1]) = (table[1], playerOne[1]);
        (playerOne[2], table[2]) = (table[2], playerOne[2]);
        currentPlayerSelectedCard = 0;
        currentTableSelectedCard = 0;
        currentTurn = 1;
        UpdateCardVisuals();
    }

    public void Skip()
    {
        if (currentTurn !=0|| gameEnded)
        {
            return;
        }

        currentTurn = 1;
        UpdateCardVisuals();
    }
    public void Knock()
    {
        if (currentTurn !=0|| gameEnded|| firstRound)
        {
            return;
        }

        lastRound = true;
        currentTurn = 1;
        UpdateCardVisuals();
    }
    public void Swap()
    {
        if (currentTurn !=0|| gameEnded)
        {
            return;
        }
        if (currentPlayerSelectedCard==0 || currentTableSelectedCard==0)
        {
            return;
        }
        (playerOne[currentPlayerSelectedCard - 1], table[currentTableSelectedCard-1]) = (table[currentTableSelectedCard-1], playerOne[currentPlayerSelectedCard - 1]);
        currentPlayerSelectedCard = 0;
        currentTableSelectedCard = 0;
        currentTurn = 1;
        UpdateCardVisuals();
    }

    public void UpdateCardVisuals()
    {
        for (var index = 0; index < playerCards.Count; index++)
        {
            playerCards[index].UpdateCard(playerOne[index],currentPlayerSelectedCard);
        }
        for (var index = 0; index < tableCards.Count; index++)
        {
            tableCards[index].UpdateCard(table[index],currentTableSelectedCard);
        }

        scoreText.text = "Score: " + CalculateScore(playerOne[0],playerOne[1],playerOne[2]);
    }
    public Card GetPlayerCard(int cardNumber)
    {
        return playerOne[cardNumber - 1];
    }

    public float CalculateScore(Card c1,Card c2, Card c3)
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
                break;
            case Face.Eight:
                return 8;
                break;
            case Face.Nine:
                return 9;
                break;
            case Face.Ten:
                return 10;
                break;
            case Face.Jack:
                return 10;
                break;
            case Face.Queen:
                return 10;
                break;
            case Face.King:
                return 10;
                break;
            case Face.Ace:
                return 11;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(face), face, null);
        }
        return 7;
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
                        (playerTwo[0], table[0]) = (table[0], playerTwo[0]);
                        currentTurn = 2;
                        UpdateCardVisuals();
                        break;
                    case 2:
                        (playerTwo[1], table[1]) = (table[1], playerTwo[1]);
                        currentTurn = 3;
                        UpdateCardVisuals();
                        break;
                    case 3:
                        (playerTwo[2], table[2]) = (table[2], playerTwo[2]);
                        currentTurn = 0;
                        firstRound = false;
                        UpdateCardVisuals();
                        if (lastRound)
                        {
                            DisplayScores();
                        }
                        break;
                    default:
                        break;
                }
            }
           
        }
    }

    private void DisplayScores()
    {
        p2Text.text = "Score: " + CalculateScore(playerTwo[0],playerTwo[1],playerTwo[2]);
        
        p3Text.text = "Score: " + CalculateScore(playerThree[0],playerThree[1],playerThree[2]);
        
        p4Text.text = "Score: " + CalculateScore(playerFour[0],playerFour[1],playerFour[2]);
        gameEnded = true;
        currentTurn = 0;
    }
}
[Serializable]
public class Card
{
    public Suit suit;
    public Face face;

    public Card(Suit suit, Face face)
    {
        this.suit = suit;
        this.face = face;
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