using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SwimmerManager manager;
    [SerializeField] private GameObject frontside,backside;
    
    [SerializeField] private bool hidden;
    
    public Suit suit;
    public Face face;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 previousPosition;
    [SerializeField] private float timer, MoveTime;
    
    
    private void Start()
    {
        manager = FindObjectOfType<SwimmerManager>();
        manager.MoveCards += MoveCard;
        
    }

    private void MoveCard()
    {
        previousPosition = transform.position;
        StartCoroutine(SlideCard());
    }


    public void Update()
    {
        frontside.SetActive(!hidden);
        backside.SetActive(hidden);
    }


    public void SetPosition(Vector3 transformPosition)
    {
        position = transformPosition;
    }

    public void Hide()
    {
        hidden = true;
    }
    public void Show()
    {
        hidden = false;
    }
    
    IEnumerator SlideCard()
    {
        for (int i = 0; i < 1; i++)
        {
            transform.position = Vector3.Lerp(previousPosition,position,i);
            yield return new WaitForSeconds(.1f);
        }

        transform.position = position;
    }
}
