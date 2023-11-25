using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private SwimmerManager _swimmerManager;
    [SerializeField] private int cardNumber;
    [SerializeField] private Card card;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image buttonBackground;
    
    private void Start()
    {
        //_swimmerManager = FindObjectOfType<SwimmerManager>();
       // UpdateCard(_swimmerManager.GetPlayerCard(cardNumber));
       buttonBackground = GetComponent<Image>();
    }

    public void UpdateCard(Card newCard, int currentPlayerSelectedCard)
    {
        card = newCard;
        text.text = card.face.ToString() +" - "+ card.suit.ToString();

        buttonBackground.color = currentPlayerSelectedCard == cardNumber ? Color.yellow : Color.white;

    }
}
