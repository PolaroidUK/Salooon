using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SwimmerManager : MonoBehaviour
{
    public int currentPlayerSelectedCard,currentTableSelectedCard;
    [SerializeField] private int currentTurn;
    [SerializeField] public bool firstRound =true;
    
    [SerializeField] private bool lastRound;
    [SerializeField] public bool gameEnded;

    [SerializeField] private float aiTimer;
    [SerializeField] private float aiTurnTime = 1;
    [SerializeField] private List<Card> deck;
    [SerializeField] private Dog[] dogs = new Dog[3];
    [SerializeField] private Table table;
    [SerializeField] private Hoof hoof;
    [SerializeField] private int lastSkipId;
    [SerializeField] private int knocker;
    [SerializeField] private GameObject discardPile;
    [SerializeField] private bool wonSchwimmerless;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private bool playerSkipped;
    [SerializeField] private ScoreText[] scores;
    

    public event Action MoveCards;
    public event Action UpdateHolders;

    private void Start()
    {
        StartRound();
    }
    public void ShuffleDeck(List<Card> list)
    {
        List<Card> temp = new List<Card>();
        temp.AddRange(list);

        for (int i = 0; i < list.Count; i++)
        {
            int index = Random.Range(0, temp.Count - 1);
            deck.Add(temp[index]);
            temp.RemoveAt(index);
        }
    }
    private void StartRound()
    {
        gameEnded = false;
        firstRound = true;
        lastRound = false;
        foreach (ScoreText scoreText in scores)
        {
            scoreText.Hide();
        }
        hoof.UpdateLivesSprites();
        
        ShuffleDeck(GetComponentsInChildren<Card>().ToList());
        foreach (Card card in deck)
        {
            card.SetPosition(transform.position);
            card.Hide();
        }

        foreach (Dog dog in dogs)
        {
            dog.UpdateLivesSprites();
            if (!dog.IsOutOfGame())
            {
                dog.GiveCards(Draw(), Draw(), Draw());
            }
        }
        
        table.GiveCards(Draw(), Draw(), Draw());
        hoof.GiveCards(Draw(), Draw(), Draw());
        MoveCards?.Invoke();
    }

    public Card Draw()
    {
        Card drawnCard = deck[0];
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
        playerSkipped = false;
        EndTurn();
    }

    public void Skip()
    {
        if (currentTurn !=0|| gameEnded)
        {
            return;
        }
        print("Skipping");
        
        playerSkipped = true;
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
        
        playerSkipped = false;
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
        
        playerSkipped = false;
        EndTurn();
    }

    private void EndTurn()
    {
        currentPlayerSelectedCard = 0;
        currentTableSelectedCard = 0;
        currentTurn = 1;
        EndTurnCheck();
    }

    public static float CalculateScore(Card c1,Card c2, Card c3)
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
    
    public static float FaceConvert(Face face)
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
                        if (currentTurn ==knocker && lastRound)
                        {
                            EndRound();
                            return;
                        }
                        TakeDogTurn(dogs[0]);
                        currentTurn = 2;
                        EndTurnCheck();
                        break;
                    case 2:
                        if (currentTurn ==knocker && lastRound)
                        {
                            EndRound();
                            return;
                        }
                        TakeDogTurn(dogs[1]);
                        currentTurn = 3;
                        EndTurnCheck();
                        break;
                    case 3:
                        if (currentTurn ==knocker && lastRound)
                        {
                            EndRound();
                            return;
                        }
                        TakeDogTurn(dogs[2]);
                        currentTurn = 0;
                        firstRound = false;
                        EndTurnCheck();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void EndTurnCheck()
    {
        if (playerSkipped&&dogs[0].skipped&&dogs[1].skipped&&dogs[2].skipped)
        {
            table.DiscardCards(discardPile);
            table.GiveCards(Draw(), Draw(), Draw());
            playerSkipped = false;
            dogs[0].skipped = false;
            dogs[1].skipped = false;
            dogs[2].skipped = false;
        }
        if (CalculateScore(hoof.GetCard(1),hoof.GetCard(2),hoof.GetCard(3))>30.5f)
        {
            UpdateHolders?.Invoke();
            MoveCards?.Invoke();
            EndRound();
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            if (CalculateScore(dogs[i].GetCard(0), dogs[i].GetCard(1), dogs[i].GetCard(2))>30.5f)
            {
                UpdateHolders?.Invoke();
                MoveCards?.Invoke();
                EndRound();
                return;
            }
        }
        UpdateHolders?.Invoke();
        MoveCards?.Invoke();
        if (lastRound && currentTurn==knocker)
        {
            EndRound();
        }
    }

    private void TakeDogTurn(Dog dog)
    {
        if (dog.IsOutOfGame())
        {
            return;
        }
        int makeMove = dog.MakeMove(table,lastRound);
        
        print(makeMove);
        switch (makeMove)
        {
            case 0:
                if (lastSkipId > 0)
                {
                    lastSkipId = currentTurn;
                }
                break;
            case 1:
                lastSkipId = -1;
                break;
            case 2:
                lastRound = true;
                knocker = currentTurn;
                lastSkipId = -1;
                break;
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

        float lowestScore = CalculateScore(hoof.GetCard(1),hoof.GetCard(2),hoof.GetCard(3));
        int lowestI = 0;
        scores[0].ShowScore(lowestScore);
        for (int i = 0; i < 3; i++)
        {
            if (dogs[i].IsOutOfGame())
            {
                continue;
            }

            float s = CalculateScore(dogs[i].GetCard(0), dogs[i].GetCard(1), dogs[i].GetCard(2));
            scores[1+i].ShowScore(s);
            if (lowestScore>s)
            {
                lowestScore = s;
                lowestI = i + 1;
            }
        }
        if (lowestI ==0)
        {
            hoof.TakeLife();
            
        }
        else
        {
            dogs[lowestI - 1].TakeLife();
            if (dogs[0].IsOutOfGame() && dogs[1].IsOutOfGame() && dogs[2].IsOutOfGame() )
            {
                stats.hasWonSchwimmerless = true;
                SceneManager.LoadScene("Saloon");
            }
        }

        
    }

    public void NewRound()
    {
        StartRound();
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